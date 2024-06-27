using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HintDB : ScriptableObject
{ 
    public List<Hint> hintList = new List<Hint>();

    /// <summary>
    /// DBからデータを取得。データがない場合はnullを返す
    /// </summary>
    /// <returns>
    /// 現在のインベントリに全てのrequiredItemが含まれており、かつblockedItemが含まれていないデータを返す
    /// </returns>
    public Hint Get()
    {
        foreach (Hint hint in hintList)
        {
            if(hint.statusTag != HintPanel.instance.statusTag) { continue; }

            bool hasAllRequiredItem = true;

            // requiredItemがすべて含まれているか確認
            for (int i = 0; i < hint.requiredItems.Length; i++)
            {
                bool hasRequiredItem = false;
                foreach (Item item in Inventory.instance.items)
                {
                    if (hint.requiredItems[i] == item.key)
                    {
                        hasRequiredItem = true;
                    }
                }
                if (!hasRequiredItem)
                {
                    hasAllRequiredItem = false;
                    break;
                }
            }
            
            if(hasAllRequiredItem) { return hint; }
        }
        return null;
    }
}
