using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenResultPanelEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(OpenResultPanelEvent) + " >")]
    [Space(10)]

    [SerializeField] float delayTime;

    /// <summary>
    /// イベントを実行
    /// メッセージウィンドウを表示
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);
            ResultPanel.instance.Open(ct);
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(OpenResultPanelEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }
}