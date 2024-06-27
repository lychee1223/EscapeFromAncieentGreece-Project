using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 逐次的に経由地点へオブジェクトを移動させ、地点A(初期値)から地点Bにオブジェクトを移動させるイベント
/// </summary>
public class MoveObjectEvent : IEvent
{
    /// <summary>
    /// 経由地点の相対位置と移動時間を格納するクラス
    /// </summary>
    [System.Serializable]
    class ViaPoint
    {
        public Vector3 targetRelativePosition;  // 経由地点の相対Position
        public Vector3 targetRelativeRotation;  // 経由地点の相対Rotation
        public float moveTime;                  // 移動時間
    }

    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(MoveObjectEvent) + " >")]
    [Space(10)]

    [SerializeField] GameObject movedObject;
    [SerializeField] List<ViaPoint> viaPoint = new List<ViaPoint>();   // 経由地点 地点A:moveProcess[0] 地点B:moveProcess[last]
    [SerializeField] bool canReturn;     // 地点Bから地点Aに戻ることが可能か

    bool hasMoved = false;  // 地点Bにいるか否か
    bool canMove = true;    // 動作可能か    

    /// <summary>
    /// イベントを実行
    /// movedObjectをmoveTimeで地点A、地点B間を移動させる
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async void Play(CancellationToken ct = default)
    {
        if (!canMove) { return; }
        if (movedObject == null) { return; }

        canMove = false;

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            movedObject.GetCancellationTokenOnDestroy(),
            ct);

        try
        {

            // 地点B -> 地点A へ移動
            if (hasMoved)
            {
                if (!canReturn) { return; }

                // moveProcessを逆順に辿り、movedObjectを逐次的に移動させる
                viaPoint.Reverse();
                foreach (ViaPoint process in viaPoint)
                {
                    await UniTask.WhenAll(
                        movedObject.transform.AddLocalPosition(-1.0f * process.targetRelativePosition, process.moveTime, linkedTokenSource.Token),
                        movedObject.transform.AddLocalRotation(-1.0f * process.targetRelativeRotation, process.moveTime, linkedTokenSource.Token));
                }
                viaPoint.Reverse();
                hasMoved = false;
            }

            // 地点A -> 地点B へ移動
            else
            {
                // moveProcessを順に辿り、movedObjectを逐次的に移動させる
                foreach (ViaPoint process in viaPoint)
                {
                    await UniTask.WhenAll(
                        movedObject.transform.AddLocalPosition(process.targetRelativePosition, process.moveTime, linkedTokenSource.Token),
                        movedObject.transform.AddLocalRotation(process.targetRelativeRotation, process.moveTime, linkedTokenSource.Token));
                }
                hasMoved = true;
            }
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(MoveObjectEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }

        canMove = true;
    }
}
