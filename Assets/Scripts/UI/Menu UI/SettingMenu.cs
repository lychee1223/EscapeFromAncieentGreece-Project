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
    public static SettingMenu instance; // �V���O���g��

    [Header("�T�E���h�ݒ�")]
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

    [Header("[����]")]
    [SerializeField] Button environmentalLeftButton;
    [SerializeField] Button environmentalRightButton;
    [SerializeField] TMP_Text environmentalText;
    [SerializeField] Image environmentalFillArea;

    [field: SerializeField] public float environmentalVolume { get; private set; }

    [SerializeField] Button backButton;

    Canvas canvas;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);
    } 

/// <summary>
/// ������
/// </summary>
void Start()
    {
        canvas = GetComponent<Canvas>();

        // �e�L�X�g�ƃv���O���X�o�[�̏�����
        bgmText.text = (bgmVolume * 100).ToString("N0") + "%";
        bgmFillArea.fillAmount = bgmVolume;

        seText.text = (seVolume * 100).ToString("N0") + "%";
        seFillArea.fillAmount = seVolume;

        environmentalText.text = (environmentalVolume * 100).ToString("N0") + "%";
        environmentalFillArea.fillAmount = environmentalVolume;

        // �T�E���h�ݒ�̊e�X���C�_�[�ɃC�x���g���A�^�b�`
        bgmLeftButton.onClick.AddListener(() => bgmVolume = ChangeVolume(bgmVolume, soundVolumeWeight * -1.0f, bgmText, bgmFillArea));
        bgmRightButton.onClick.AddListener(() => bgmVolume = ChangeVolume(bgmVolume, soundVolumeWeight * 1.0f, bgmText, bgmFillArea));

        seLeftButton.onClick.AddListener(() => seVolume = ChangeVolume(seVolume, soundVolumeWeight * -1.0f, seText, seFillArea));
        seRightButton.onClick.AddListener(() => seVolume = ChangeVolume(seVolume, soundVolumeWeight * 1.0f, seText, seFillArea));

        environmentalLeftButton.onClick.AddListener(() => environmentalVolume = ChangeVolume(environmentalVolume, soundVolumeWeight * -1.0f, environmentalText, environmentalFillArea));
        environmentalRightButton.onClick.AddListener(() => environmentalVolume = ChangeVolume(environmentalVolume, soundVolumeWeight * 1.0f, environmentalText, environmentalFillArea));

        // �߂�{�^���ɃC�x���g���A�^�b�`
        backButton.onClick.AddListener(() => Close(this.GetCancellationTokenOnDestroy()));
    }

    void Update()
    {
        if (!canvas.enabled) { return; }

        // ESC�L�[�ŕ���
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
            await UniTask.DelayFrame(1, cancellationToken: ct); // pause��ʂ������ɔ�����̂�h������Delay
            canvas.enabled = false;
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
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
