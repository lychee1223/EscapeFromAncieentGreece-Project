using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDB : ScriptableObject
{
    public List<Item> itemList = new List<Item>();

    /// <summary>
    /// DB����f�[�^���擾�B�f�[�^���Ȃ��ꍇ��null��Ԃ�
    /// </summary>
    /// <param name="key">�擾����A�C�e����key</param>
    /// <returns>
    /// angleList��key�ɑΉ�����f�[�^�����݂���   => �f�[�^��Ԃ�
    ///                                 ���݂��Ȃ� => null��Ԃ�
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