using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SetStatusTagEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(SetStatusTagEvent) + " >")]
    [Space(10)]

    [SerializeField] Hint.Tag statusTag;

    /// <summary>
    /// イベントを実行
    /// メッセージウィンドウを表示
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        HintPanel.instance.statusTag = statusTag;
    }
}
