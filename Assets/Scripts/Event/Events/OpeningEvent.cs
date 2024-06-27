using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class OpeningEvent : MonoBehaviour
{
    public static OpeningEvent instance;    // �V���O���g��

    [SerializeField] CanvasGroup prologueCanvas;
    [SerializeField] TMP_Text prologueText;
    [SerializeField] TMP_Text triangle;

    [SerializeField] float textSpead;
    bool shouldSkip;
    [MultilineAttribute]
    [SerializeField] string[] prologueTexts;

    [SerializeField] float closeTime;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldSkip = true;
        }
    }

    /// <summary>
    /// �C�x���g�����s
    /// �v�����[�O��\������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async UniTask Play(CancellationToken ct = default)
    {
        // �v�����[�O�̃e�L�X�g�����ɍĐ�
        foreach (string text in prologueTexts)
        {
            prologueText.maxVisibleCharacters = 0;
            triangle.enabled = false;
            shouldSkip = false;

            prologueText.text = text;

            // �ő�\�����������X�V���A�e�L�X�g��1�������\��
            for (var i = 1; i <= prologueText.text.Length; i++)
            {
                if (shouldSkip)
                {
                    prologueText.maxVisibleCharacters = prologueText.text.Length;
                    break;
                }

                prologueText.maxVisibleCharacters = i;
                await UniTask.Delay(System.TimeSpan.FromSeconds(textSpead), cancellationToken: ct);
            }

            // ���N���b�N�����������܂őҋ@
            triangle.enabled = true;

            await UniTask.DelayFrame(1, cancellationToken: ct);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken:ct);
            await UniTask.DelayFrame(1, cancellationToken: ct);
        }

        // Canvas���\���ɂ���
        await prologueCanvas.MakeTransparent(closeTime, ct);
        prologueCanvas.gameObject.SetActive(false);
    }
}
