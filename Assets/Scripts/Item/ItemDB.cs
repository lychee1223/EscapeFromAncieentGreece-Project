using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDB : ScriptableObject
{
    public List<Item> itemList = new List<Item>();

    /// <summary>
    /// DBからデータを取得。データがない場合はnullを返す
    /// </summary>
    /// <param name="key">取得するアイテムのkey</param>
    /// <returns>
    /// angleListにkeyに対応するデータが存在する   => データを返す
    ///                                 存在しない => nullを返す
    /// </returns>
    public Item Get(Item.ItemKey key)
    {
        foreach (Item item in itemList)
        {
            if (item.key == key) { return item; }
        }

        return null;
    }
}