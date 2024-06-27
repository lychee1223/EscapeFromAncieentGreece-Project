using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCameraAngleEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(ChangeCameraAngleEvent) + " >")]
    [Space(10)]

    [SerializeField] CameraAngle.AngleKey destinationAngle;
    [SerializeField] float delayTime;

    /// <summary>
    /// イベントを実行
    /// カメラアングルを変化させる
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);
            CameraManagaer.instance.ChangeCameraAngle(destinationAngle);
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(ChangeCameraAngleEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }
}
