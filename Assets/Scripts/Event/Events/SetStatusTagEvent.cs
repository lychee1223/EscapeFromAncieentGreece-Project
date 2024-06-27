using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SetStatusTagEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(SetStatusTagEvent) + " >")]
    [Space(10)]

    [SerializeField] Hint.Tag statusTag;

    /// <summary>
    /// �C�x���g�����s
    /// ���b�Z�[�W�E�B���h�E��\��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        HintPanel.instance.statusTag = statusTag;
    }
}
