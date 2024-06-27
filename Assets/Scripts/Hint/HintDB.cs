using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HintDB : ScriptableObject
{ 
    public List<Hint> hintList = new List<Hint>();

    /// <summary>
    /// DB����f�[�^���擾�B�f�[�^���Ȃ��ꍇ��null��Ԃ�
    /// </summary>
    /// <returns>
    /// ���݂̃C���x���g���ɑS�Ă�requiredItem���܂܂�Ă���A����blockedItem���܂܂�Ă��Ȃ��f�[�^��Ԃ�
    /// </returns>
    public Hint Get()
    {
        foreach (Hint hint in hintList)
        {
            if(hint.statusTag != HintPanel.instance.statusTag) { continue; }

            bool hasAllRequiredItem = true;

            // requiredItem�����ׂĊ܂܂�Ă��邩�m�F
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
