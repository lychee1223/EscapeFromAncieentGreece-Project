using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance; // ƒVƒ“ƒOƒ‹ƒgƒ“

    [field: SerializeField] public AudioSource audioSource { get; private set; }
    [SerializeField] AudioClip titleBGM;
    [SerializeField] AudioClip gameBGM;

    public bool isUseSettingsVolume { get; set; } = true;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void Update()
    {
        if (!isUseSettingsVolume) { return; }

        audioSource.volume = SettingMenu.instance.bgmVolume;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        isUseSettingsVolume = true;

        if (nextScene.name == "GameScene")
        {
            audioSource.clip = gameBGM;

        }
        if (nextScene.name == "TitleScene")
        {
            audioSource.clip = titleBGM;
        }

        audioSource.Play();
    }
}
