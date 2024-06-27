using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSoundSource : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void Update()
    {
        if (!GameManager.instance.isPlayable || Time.timeScale == 0.0f)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
            audioSource.volume = SettingMenu.instance.environmentalVolume;
        }
    }
}
