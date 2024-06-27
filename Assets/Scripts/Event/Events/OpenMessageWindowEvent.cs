using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMessageWindowEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(OpenMessageWindowEvent) + " >")]
    [Space(10)]

    [SerializeField] MessageWindow messageWindow;

    /// <summary>
    /// イベントを実行
    /// メッセージウィンドウを表示
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        messageWindow.Open(ct);
    }
}
