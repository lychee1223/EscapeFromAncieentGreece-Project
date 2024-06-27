using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    Canvas canvas;
    [SerializeField] CanvasGroup disableCanvasGroup;

    [SerializeField] Button toHowToButton;
    [SerializeField] Button toSettingButton;
    [SerializeField] Button toTitleButton;

    [SerializeField] Button toGameButton;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();

        // 各ボタンにイベントをアタッチ
        toHowToButton.onClick.AddListener(() => HowToPage.instance.Open());
        toSettingButton.onClick.AddListener(() => SettingMenu.instance.Open());
        toTitleButton.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
        toGameButton.onClick.AddListener(() => Close());
    }

    public void Open()
    {
        Time.timeScale = 0.0f;

        canvas.enabled = true;
        disableCanvasGroup.alpha = 0.0f;
        disableCanvasGroup.interactable = false;
    }

    public void Close()
    {
        canvas.enabled = false;
        disableCanvasGroup.alpha = 1.0f;
        disableCanvasGroup.interactable = true;

        Time.timeScale = 1.0f;
    }

}
