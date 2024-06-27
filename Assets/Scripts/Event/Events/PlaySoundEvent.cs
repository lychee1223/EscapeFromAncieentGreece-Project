using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlaySoundEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(PlaySoundEvent) + " >")]
    [Space(10)]

    [SerializeField] AudioClip audioClip;

    /// <summary>
    /// �C�x���g�����s
    /// �������Đ�
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Play(CancellationToken ct = default)
    {
        float volume = SettingMenu.instance.seVolume;
        SoundEffects.instance.audioSource.PlayOneShot(audioClip, volume);
    }
}
