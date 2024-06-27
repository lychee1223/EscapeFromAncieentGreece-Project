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

    [Header("���j���[")]
    [SerializeField] CanvasGroup menuCanvas;
    [SerializeField] Button toGameButton;
    [SerializeField] Button toHowToButton;
    [SerializeField] Button toSettingButton;
    [SerializeField] Button quitButton;

    [Header("���j���[�̏o���A�j���[�V����")]
    [SerializeField] Vector3 targetRelativePosition;
    [SerializeField] float openTime;

    [Header("�Q�[���V�[���ւ̉�ʑJ��")]
    [SerializeField] Image panel;
    [SerializeField] Image edge;
    [SerializeField] float edgeWidth;
    [SerializeField] float transitionTime;
    [SerializeField] TMP_Text nowLoadingText;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        Time.timeScale = 1;

        // �e�{�^���ɃC�x���g���A�^�b�`
        toGameButton.onClick.AddListener(() => ToGame(this.GetCancellationTokenOnDestroy()));
        toHowToButton.onClick.AddListener(() => HowToPage.instance.Open());
        toSettingButton.onClick.AddListener(() => SettingMenu.instance.Open());
        quitButton.onClick.AddListener(() => Quit());
    }

    /// <summary>
    /// �C�ӂ̓��͂��󂯕t����
    /// </summary>
    private void Update()
    {
        // �C�ӂ̓��͂��󂯕t���A���j���[��\������
        if (isWaitPressAnyButton && Input.anyKey)
        {
            isWaitPressAnyButton = false;
            Open(this.GetCancellationTokenOnDestroy());
        }
    }

    /// <summary>
    /// ���j���[��\������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
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
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // �����[�h
            return;
        }

        menuCanvas.interactable = true;
    }

    /// <summary>
    /// �V�[����J�ڂ��ăQ�[�����J�n
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    async void ToGame(CancellationToken ct = default)
    {
        float currentTime = 0.0f;   // �o�ߎ���
        float rate;                 // �V�[���J�ڃp�l����fillAmount

        menuCanvas.interactable = false;    // ���j���[�{�^��������

        ColorBlock colors = toGameButton.colors;
        colors.disabledColor = colors.highlightedColor;
        toGameButton.colors = colors;
        BackgroundMusic.instance.isUseSettingsVolume = false;

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            panel.gameObject.GetCancellationTokenOnDestroy(),
            edge.GetCancellationTokenOnDestroy(),
            ct);

        // transitionTime�b�ŉ�ʑJ�ڃp�l����\��
        while (currentTime <= transitionTime)
        {
            // �o�ߎ��ԍX�V
            currentTime += Time.deltaTime;

            // �V�[���J�ڃp�l����fillAmount�X�V
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
            // UniTask�̃L�����Z����catch
            catch (OperationCanceledException e)
            {
#if UNITY_EDITOR
                Debug.Log(e);
#endif

                SceneManager.LoadScene("TitleScene");    // �����[�h
                return;
            }
        }

        nowLoadingText.gameObject.SetActive(true);

        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// �A�v���P�[�V�����𗎂Ƃ�
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
