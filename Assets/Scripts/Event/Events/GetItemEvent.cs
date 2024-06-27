using System.Threading;
using UnityEngine;

public class GetItemEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(GetItemEvent) + " >")]
    [Space(10)]

    [SerializeField] Item.ItemKey replacedItem;
    [SerializeField] Item.ItemKey gottenItem;
    [SerializeField] bool shouldSyncRotation;

    /// <summary>
    /// �C�x���g�����s
    /// �A�C�e�������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        // �V�K����
        if(replacedItem == Item.ItemKey.NONE)
        {
            Inventory.instance.Get(gottenItem);
        }
        // �u�����ē���
        else
        {
            Inventory.instance.Replace(replacedItem, gottenItem, shouldSyncRotation);
        }
    }
}
