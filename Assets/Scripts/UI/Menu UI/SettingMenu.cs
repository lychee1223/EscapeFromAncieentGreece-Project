using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu instance; // シングルトン

    [Header("サウンド設定")]
    [SerializeField] float soundVolumeWeight;

    [Header("[BGM]")]
    [SerializeField] Button bgmLeftButton;
    [SerializeField] Button bgmRightButton;
    [SerializeField] TMP_Text bgmText;
    [SerializeField] Image bgmFillArea;
    [field: SerializeField] public float bgmVolume { get; private set; }

    [Header("[SE]")]
    [SerializeField] Button seLeftButton;
    [SerializeField] Button seRightButton;
    [SerializeField] TMP_Text seText;
    [SerializeField] Image seFillArea;
    [field: SerializeField] public float seVolume { get; private set; }

    [Header("[環境音]")]
    [SerializeField] Button environmentalLeftButton;
    [SerializeField] Button environmentalRightButton;
    [SerializeField] TMP_Text environmentalText;
    [SerializeField] Image environmentalFillArea;

    [field: SerializeField] public float environmentalVolume { get; private set; }

    [SerializeField] Button backButton;

    Canvas canvas;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);
    } 

/// <summary>
/// 初期化
/// </summary>
void Start()
    {
        canvas = GetComponent<Canvas>();

        // テキストとプログレスバーの初期化
        bgmText.text = (bgmVolume * 100).ToString("N0") + "%";
        bgmFillArea.fillAmount = bgmVolume;

        seText.text = (seVolume * 100).ToString("N0") + "%";
        seFillArea.fillAmount = seVolume;

        environmentalText.text = (environmentalVolume * 100).ToString("N0") + "%";
        environmentalFillArea.fillAmount = environmentalVolume;

        // サウンド設定の各スライダーにイベントをアタッチ
        bgmLeftButton.onClick.AddListener(() => bgmVolume = ChangeVolume(bgmVolume, soundVolumeWeight * -1.0f, bgmText, bgmFillArea));
        bgmRightButton.onClick.AddListener(() => bgmVolume = ChangeVolume(bgmVolume, soundVolumeWeight * 1.0f, bgmText, bgmFillArea));

        seLeftButton.onClick.AddListener(() => seVolume = ChangeVolume(seVolume, soundVolumeWeight * -1.0f, seText, seFillArea));
        seRightButton.onClick.AddListener(() => seVolume = ChangeVolume(seVolume, soundVolumeWeight * 1.0f, seText, seFillArea));

        environmentalLeftButton.onClick.AddListener(() => environmentalVolume = ChangeVolume(environmentalVolume, soundVolumeWeight * -1.0f, environmentalText, environmentalFillArea));
        environmentalRightButton.onClick.AddListener(() => environmentalVolume = ChangeVolume(environmentalVolume, soundVolumeWeight * 1.0f, environmentalText, environmentalFillArea));

        // 戻るボタンにイベントをアタッチ
        backButton.onClick.AddListener(() => Close(this.GetCancellationTokenOnDestroy()));
    }

    void Update()
    {
        if (!canvas.enabled) { return; }

        // ESCキーで閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close(this.GetCancellationTokenOnDestroy());
        }
    }

    public void Open()
    {
        canvas.enabled = true;
    }

    public async void Close(CancellationToken ct = default)
    {
        try
        {
            await UniTask.DelayFrame(1, cancellationToken: ct); // pause画面も同時に抜けるのを防ぐためDelay
            canvas.enabled = false;
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }

    float ChangeVolume(float volume, float addend, TMP_Text text, Image fillArea)
    {
        volume = Mathf.Clamp01(volume + addend);
        Debug.Log(volume);
        fillArea.fillAmount = volume;
        text.text = (volume * 100).ToString("N0") + "%";
        return volume;
    }
}
