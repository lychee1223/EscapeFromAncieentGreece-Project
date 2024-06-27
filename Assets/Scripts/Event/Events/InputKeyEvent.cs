using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class InputKeyEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(InputKeyEvent) + " >")]
    [Space(10)]

    [SerializeField] ClickInputIF inputIF;

    /// <summary>
    /// �C�x���g�����s
    /// inputIF����lockSystem�ɓ��͐M���𑗂�
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        inputIF.Input(ct);
    }
}
