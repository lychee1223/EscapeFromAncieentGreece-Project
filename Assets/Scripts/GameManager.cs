using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //シングルトン

    // スコア
    public int risk { get; private set; } = 0;      // 感染リスク(クリック回数)
    public float second { get; private set; } = 0;  // 経過した秒数
    public int fundage { get; private set; } = 0;   // 取得したドラクマの金額

    [SerializeField] int maxRisk;

    [SerializeField] Image infectionEffect;

    [SerializeField] CameraAngle.AngleKey gameClearAngleKey;
    [SerializeField] EventList gameClearEventList;
    [SerializeField] EventList gameOverEventList;

    public bool isPlayable { get; private set; } = false;

    Canvas resulutPanelCanvas;
    Canvas settingMenuCanvas;
    Canvas howToPageCanvas;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    async void Start()
    {
        resulutPanelCanvas = ResultPanel.instance.gameObject.GetComponent<Canvas>();
        settingMenuCanvas = SettingMenu.instance.gameObject.GetComponent<Canvas>();
        howToPageCanvas = HowToPage.instance.gameObject.GetComponent<Canvas>();


        Color color = infectionEffect.color;
        color.a = 0.0f;
        infectionEffect.color = color;

        Time.timeScale = 1.0f;

        try
        {
            // プロローグを再生
            await OpeningEvent.instance.Play(this.GetCancellationTokenOnDestroy());
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(GameManager) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }

        isPlayable = true;
    }

    /// <summary>
    /// ゲーム中の各キー入力を受け付け, ゲームオーバー判定を行う
    /// </summary>
    void Update()
    {
        // ESCキーでメニューの表示を切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!resulutPanelCanvas.enabled)
            {
                if ((Time.timeScale != 0.0f))
                {
                    PauseMenu.instance.Open();
                }
                else if (!settingMenuCanvas.enabled && !howToPageCanvas.enabled)
                {
                    PauseMenu.instance.Close();
                }
            }
        }

        if (!isPlayable || Time.timeScale == 0.0f) { return; }    // 以降の処理はポーズ中行わない
        
        // パラメータ更新    
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseRisk(); // 左クリックで感染リスク増加
        }

        IncreaseTime(); // 経過時間を増加

        // ゲームオーバー処理
        if (CameraManagaer.instance.currentAngle == gameClearAngleKey)
        {
            GameOver(true, this.GetCancellationTokenOnDestroy());  // ゴール到達でゲームクリア
        }

        if (risk / maxRisk == 1.0f)
        {
            GameOver(false, this.GetCancellationTokenOnDestroy());  // 感染率100%でゲームオーバー
        }
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    /// <param name="playedEventList">再生するイベントリスト</param>
    void GameOver(bool isClear, CancellationToken ct = default)
    {
        isPlayable = false;

        float rate = (float)risk / maxRisk;
        ResultPanel.instance.SetStatus(rate, fundage, second, isClear);
        ScoreManager.instance.CalculateScore(rate, fundage, second, isClear);

        if (isClear)
        {
            gameClearEventList.Play(ct);
        }
        else
        {
            gameOverEventList.Play(ct);
        }
    }

    /// <summary>
    /// 感染リスクを増加させる
    /// </summary>
    /// <param name="increaseAmount">感染リスクの増加量</param>
    void IncreaseRisk()
    {
        risk = Math.Min(risk + 1, maxRisk);
        float rate = (float)risk / maxRisk;
        RiskGauge.instance.Set(rate);

        // エフェクトの透過率を更新
        Color color = infectionEffect.color;
        color.a = ((float)Math.Pow(2, rate) - 1.0f) * 0.2f;
        infectionEffect.color = color;
    }

    /// <summary>
    /// 経過時間を増加させる
    /// </summary>
    void IncreaseTime()
    {
        second += Time.deltaTime;
        TimeCounter.instance.Set(second);
    }

    /// <summary>
    /// 所持金を増加させる
    /// </summary>
    /// <param name="increaseAmount">収入</param>
    public void IncreaseDrachma(int income)
    {
        fundage += income;
        DrachmaCounter.instance.Set(fundage);
    }


}
