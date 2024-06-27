using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public static class ExtensionCanvasGroup
{
    /// <summary>
    /// オブジェクトのlocalPositionをtargetRelativePositionへtime秒で移動させる
    /// </summary>
    /// <param name="canvasGroup">CanvasGroup型を拡張</param>
    /// <param name="time">透明度の変化時間</param>
    public static async UniTask MakeOpaque(this CanvasGroup canvasGroup, float time, CancellationToken ct = default)
    {
        float initialAlpha = canvasGroup.alpha;

        float currentTime = 0.0f;   // 経過時間
        float rate;                 // alpha値の増加割合

        // closeTime秒でcanvasを透明にする
        while (currentTime <= time)
        {
            // 経過時間更新
            currentTime += Time.deltaTime;

            // alpha値の増加割合更新
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

        float currentTime = 0.0f;   // 経過時間
        float rate;                 // alpha値の減少割合

        // closeTime秒でcanvasを透明にする
        while (currentTime <= time)
        {
            // 経過時間更新
            currentTime += Time.deltaTime;

            // alpha値の減少割合更新
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
