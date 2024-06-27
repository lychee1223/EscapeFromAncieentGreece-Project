using System.Threading;
using UnityEngine;

public class GetItemEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(GetItemEvent) + " >")]
    [Space(10)]

    [SerializeField] Item.ItemKey replacedItem;
    [SerializeField] Item.ItemKey gottenItem;
    [SerializeField] bool shouldSyncRotation;

    /// <summary>
    /// イベントを実行
    /// アイテムを入手
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        // 新規入手
        if(replacedItem == Item.ItemKey.NONE)
        {
            Inventory.instance.Get(gottenItem);
        }
        // 置換して入手
        else
        {
            Inventory.instance.Replace(replacedItem, gottenItem, shouldSyncRotation);
        }
    }
}
