using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    [SerializeField] TMP_Text text;

    [Header("�������x�E�\������")]
    [SerializeField] float textSpead = 0.05f;
    [SerializeField] float aliveTime = 5.0f;
    [SerializeField] float closeTime = 1.0f;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        text.maxVisibleCharacters = 0;
        canvas.alpha = 0.0f;
    }

    /// <summary>
    /// MessageWindow��\��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Open(CancellationToken ct = default)
    {
        try
        {
            if (canvas.alpha != 0.0f) { return; }

            // canvas�̕\��
            canvas.alpha = 1.0f;

            // �ő�\�����������X�V���A�e�L�X�g��1�������\��
            for (var i = 1; i <= text.text.Length; i++)
            {
                text.maxVisibleCharacters = i;
                await UniTask.Delay(System.TimeSpan.FromSeconds(textSpead), cancellationToken: ct);
            }

            // aliveTime�b��ACanvas���\���ɂ���
            await UniTask.Delay(System.TimeSpan.FromSeconds(aliveTime), cancellationToken: ct);
            await canvas.MakeTransparent(closeTime, ct);
        }
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(MessageWindow) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}
