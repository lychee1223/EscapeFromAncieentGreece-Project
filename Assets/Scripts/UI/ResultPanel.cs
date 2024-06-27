using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public static ResultPanel instance; // シングルトン

    Canvas canvas;
    [SerializeField] CanvasGroup disableCanvasGroup;
    [SerializeField] GameObject toTitleButton;

    [SerializeField] TMP_Text riskRateText;
    [SerializeField] TMP_Text riskScoreText;

    [SerializeField] TMP_Text drachmaText;
    [SerializeField] TMP_Text drachmaScoreText;

    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text timeScoreText;

    [SerializeField] TMP_Text bonusHeaderText;
    [SerializeField] TMP_Text bonusMultiplierText;

    [SerializeField] TMP_Text totalScoreText;

    [SerializeField] float showScoreSpead;

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
    void Start()
    {
        canvas = GetComponent<Canvas>();

        // ToTileボタンにTotile()をアタッチ
        toTitleButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
    }

    void Update()
    {
        if (!canvas.enabled) { return; }

        // ESCキーでタイトルへ戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    /// <summary>
    /// リザルトを表示
    /// </summary>
    /// <param name="riskScore">感染率のスコア</param>
    /// <param name="timeScore">クリアタイムのスコア</param>
    /// <param name="drachmaScore">所持金のスコア</param>
    /// <param name="totalScore">合計スコア</param>
    /// <param name="shouldAddBonus"></param>
    public void SetStatus(float riskRate, int fundage, float second, bool isClear)
    {
        riskRateText.text = Mathf.RoundToInt(riskRate * 100f).ToString() + "%";

        drachmaText.text = fundage.ToString();

        int minute = (int)(second / 60.0f);
        second -= minute * 60;
        timeText.text = minute.ToString("00") + ":" + ((int)second).ToString("00");

        if (isClear)
        {
            bonusHeaderText.text = "クリアボーナス";
            bonusMultiplierText.text = "×" + ScoreManager.instance.clearBonus.ToString();
        }
        else
        {
            bonusHeaderText.text = "";
            bonusMultiplierText.text = "";
        }

    }

    public async void Open(CancellationToken ct = default)
    {
        try
        {
            bonusHeaderText.enabled = false;
            bonusMultiplierText.enabled = false;

            // UIを切り替え
            canvas.enabled = true;
            disableCanvasGroup.alpha = 0.0f;
            disableCanvasGroup.interactable = false;

            // スコアを表示
            await UniTask.Delay(System.TimeSpan.FromSeconds(showScoreSpead), cancellationToken: ct);
            riskScoreText.text = ScoreManager.instance.riskScore.ToString();
            drachmaScoreText.text = ScoreManager.instance.drachmaScore.ToString();
            timeScoreText.text = ScoreManager.instance.timeScore.ToString();

            if (bonusHeaderText.text != "") {
                await UniTask.Delay(System.TimeSpan.FromSeconds(showScoreSpead), cancellationToken: ct);
                bonusHeaderText.enabled = true;
                bonusMultiplierText.enabled = true;
            }
            await UniTask.Delay(System.TimeSpan.FromSeconds(showScoreSpead), cancellationToken: ct);
            totalScoreText.text = ScoreManager.instance.totalScore.ToString();
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
    }
}
