using System.Threading;
using UnityEngine;

public class GetDrachmaEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(GetDrachmaEvent) + " >")]
    [Space(10)]

    [SerializeField]int amount;

    /// <summary>
    /// �C�x���g�����s
    /// �h���N�}�����
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        GameManager.instance.IncreaseDrachma(amount);
    }
}
