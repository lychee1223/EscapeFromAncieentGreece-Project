using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public static class ExtensionCanvasGroup
{
    /// <summary>
    /// �I�u�W�F�N�g��localPosition��targetRelativePosition��time�b�ňړ�������
    /// </summary>
    /// <param name="canvasGroup">CanvasGroup�^���g��</param>
    /// <param name="time">�����x�̕ω�����</param>
    public static async UniTask MakeOpaque(this CanvasGroup canvasGroup, float time, CancellationToken ct = default)
    {
        float initialAlpha = canvasGroup.alpha;

        float currentTime = 0.0f;   // �o�ߎ���
        float rate;                 // alpha�l�̑�������

        // closeTime�b��canvas�𓧖��ɂ���
        while (currentTime <= time)
        {
            // �o�ߎ��ԍX�V
            currentTime += Time.deltaTime;

            // alpha�l�̑��������X�V
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            canvasGroup.alpha = Mathf.Lerp(initialAlpha, 1.0f, rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }

    public static async UniTask MakeTransparent(this CanvasGroup canvasGroup, float time, CancellationToken ct = default)
    {
        float initialAlpha = canvasGroup.alpha;

        float currentTime = 0.0f;   // �o�ߎ���
        float rate;                 // alpha�l�̌�������

        // closeTime�b��canvas�𓧖��ɂ���
        while (currentTime <= time)
        {
            // �o�ߎ��ԍX�V
            currentTime += Time.deltaTime;

            // alpha�l�̌��������X�V
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            canvasGroup.alpha = Mathf.Lerp(0.0f, initialAlpha, 1.0f - rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }
}
