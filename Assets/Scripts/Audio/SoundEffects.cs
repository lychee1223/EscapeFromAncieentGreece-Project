using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;    // ƒVƒ“ƒOƒ‹ƒgƒ“

    [field: SerializeField] public AudioSource audioSource { get; private set; }

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "TitleScene") { return; }

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
