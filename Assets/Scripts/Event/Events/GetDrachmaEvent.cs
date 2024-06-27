using System.Threading;
using UnityEngine;

public class GetDrachmaEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(GetDrachmaEvent) + " >")]
    [Space(10)]

    [SerializeField]int amount;

    /// <summary>
    /// イベントを実行
    /// ドラクマを入手
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        GameManager.instance.IncreaseDrachma(amount);
    }
}
