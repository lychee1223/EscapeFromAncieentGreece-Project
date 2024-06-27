using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] TMP_Text pressAnyButtonText;
    bool isWaitPressAnyButton = true;

    [Header("メニュー")]
    [SerializeField] CanvasGroup menuCanvas;
    [SerializeField] Button toGameButton;
    [SerializeField] Button toHowToButton;
    [SerializeField] Button toSettingButton;
    [SerializeField] Button quitButton;

    [Header("メニューの出現アニメーション")]
    [SerializeField] Vector3 targetRelativePosition;
    [SerializeField] float openTime;

    [Header("ゲームシーンへの画面遷移")]
    [SerializeField] Image panel;
    [SerializeField] Image edge;
    [SerializeField] float edgeWidth;
    [SerializeField] float transitionTime;
    [SerializeField] TMP_Text nowLoadingText;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        Time.timeScale = 1;

        // 各ボタンにイベントをアタッチ
        toGameButton.onClick.AddListener(() => ToGame(this.GetCancellationTokenOnDestroy()));
        toHowToButton.onClick.AddListener(() => HowToPage.instance.Open());
        toSettingButton.onClick.AddListener(() => SettingMenu.instance.Open());
        quitButton.onClick.AddListener(() => Quit());
    }

    /// <summary>
    /// 任意の入力を受け付ける
    /// </summary>
    private void Update()
    {
        // 任意の入力を受け付け、メニューを表示する
        if (isWaitPressAnyButton && Input.anyKey)
        {
            isWaitPressAnyButton = false;
            Open(this.GetCancellationTokenOnDestroy());
        }
    }

    /// <summary>
    /// メニューを表示する
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    async void Open(CancellationToken ct = default)
    {
        pressAnyButtonText.gameObject.SetActive(false);

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            menuCanvas.gameObject.GetCancellationTokenOnDestroy(),
            toGameButton.GetCancellationTokenOnDestroy(),
            toHowToButton.GetCancellationTokenOnDestroy(),
            toSettingButton.GetCancellationTokenOnDestroy(),
            quitButton.GetCancellationTokenOnDestroy(),
            ct);

        try
        {
            await UniTask.WhenAll(
                menuCanvas.MakeOpaque(openTime, linkedTokenSource.Token),
                toGameButton.transform.AddLocalPosition(targetRelativePosition, openTime, linkedTokenSource.Token),
                toHowToButton.transform.AddLocalPosition(targetRelativePosition, openTime, linkedTokenSource.Token),
                toSettingButton.transform.AddLocalPosition(targetRelativePosition, openTime, linkedTokenSource.Token),
                quitButton.transform.AddLocalPosition(targetRelativePosition, openTime, linkedTokenSource.Token));
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // リロード
            return;
        }

        menuCanvas.interactable = true;
    }

    /// <summary>
    /// シーンを遷移してゲームを開始
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    async void ToGame(CancellationToken ct = default)
    {
        float currentTime = 0.0f;   // 経過時間
        float rate;                 // シーン遷移パネルのfillAmount

        menuCanvas.interactable = false;    // メニューボタン無効化

        ColorBlock colors = toGameButton.colors;
        colors.disabledColor = colors.highlightedColor;
        toGameButton.colors = colors;
        BackgroundMusic.instance.isUseSettingsVolume = false;

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            panel.gameObject.GetCancellationTokenOnDestroy(),
            edge.GetCancellationTokenOnDestroy(),
            ct);

        // transitionTime秒で画面遷移パネルを表示
        while (currentTime <= transitionTime)
        {
            // 経過時間更新
            currentTime += Time.deltaTime;

            // シーン遷移パネルのfillAmount更新
            if (transitionTime == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / transitionTime);
            }

            panel.fillAmount = rate;
            edge.fillAmount = rate + edgeWidth;
            BackgroundMusic.instance.audioSource.volume = SettingMenu.instance.bgmVolume * (1.0f - rate);

            try
            {
                await UniTask.DelayFrame(1, cancellationToken: linkedTokenSource.Token);
            }
            // UniTaskのキャンセルをcatch
            catch (OperationCanceledException e)
            {
#if UNITY_EDITOR
                Debug.Log(e);
#endif

                SceneManager.LoadScene("TitleScene");    // リロード
                return;
            }
        }

        nowLoadingText.gameObject.SetActive(true);

        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// アプリケーションを落とす
    /// </summary>
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
