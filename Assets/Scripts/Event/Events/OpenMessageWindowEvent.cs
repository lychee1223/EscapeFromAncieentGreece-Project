using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMessageWindowEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(OpenMessageWindowEvent) + " >")]
    [Space(10)]

    [SerializeField] MessageWindow messageWindow;

    /// <summary>
    /// �C�x���g�����s
    /// ���b�Z�[�W�E�B���h�E��\��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        messageWindow.Open(ct);
    }
}
