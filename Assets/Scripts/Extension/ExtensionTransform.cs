using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class ExtensionTransform
{
    /// <summary>
    /// オブジェクトのlocalPositionをtargetRelativePositionへtime秒で移動させる
    /// </summary>
    /// <param name="transform">Transform型を拡張</param>
    /// <param name="targetRelativePosition">移動先の相対位置</param>
    /// <param name="time">移動時間</param>
    public static async UniTask AddLocalPosition(this Transform transform, Vector3 targetRelativePosition, float time, CancellationToken ct = default)
    {
        // 初期値と目標位置のlocalPositionを取得
        Vector3 fromPosition = transform.localPosition;
        Vector3 toPosition = fromPosition + targetRelativePosition;

        float currentTime = 0.0f;   // 経過時間
        float rate;                 // 移動割合

        // time 秒オブジェクトを動かしつ続ける
        while (currentTime <= time)
        {
            // 経過時間を更新
            currentTime += Time.deltaTime;

            // 移動割合を更新
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            // ベクトルを可算
            transform.localPosition = Vector3.Lerp(fromPosition, toPosition, rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }

    /// <summary>
    /// オブジェクトのlocalRotationをtargetRelativeRotationへtime秒で回転させる
    /// </summary>
    /// <param name="transform">Transform型を拡張</param>
    /// <param name="targetRelativeRotation">移動先の相対位置</param>
    /// <param name="time">移動時間</param>
    public static async UniTask AddLocalRotation(this Transform transform, Vector3 targetRelativeRotation, float time, CancellationToken ct = default)
    {
        // 初期値と目標位置のlocalRotationを取得
        Quaternion fromRotation = transform.localRotation;
        Quaternion toRotation = fromRotation * Quaternion.Euler(targetRelativeRotation);

        float currentTime = 0.0f;   // 経過時間
        float rate;                 // 移動割合

        // time 秒オブジェクトを動かしつ続ける
        while (currentTime <= time)
        {
            // 経過時間を更新
            currentTime += Time.deltaTime;

            // 移動割合を更新
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            // ベクトルを可算
            transform.localRotation = Quaternion.Slerp(fromRotation, toRotation, rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }
}
