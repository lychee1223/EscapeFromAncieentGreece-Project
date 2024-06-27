using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlaySoundEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(PlaySoundEvent) + " >")]
    [Space(10)]

    [SerializeField] AudioClip audioClip;

    /// <summary>
    /// イベントを実行
    /// 音源を再生
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Play(CancellationToken ct = default)
    {
        float volume = SettingMenu.instance.seVolume;
        SoundEffects.instance.audioSource.PlayOneShot(audioClip, volume);
    }
}
