using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class InputKeyEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(InputKeyEvent) + " >")]
    [Space(10)]

    [SerializeField] ClickInputIF inputIF;

    /// <summary>
    /// イベントを実行
    /// inputIFからlockSystemに入力信号を送る
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        inputIF.Input(ct);
    }
}
