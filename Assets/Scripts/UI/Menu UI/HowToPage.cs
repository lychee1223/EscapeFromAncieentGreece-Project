using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPage : MonoBehaviour
{
    public static HowToPage instance; // シングルトン

    [SerializeField] CanvasGroup[] pages;
    int currentPage = 0;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button backButton;
    
    Canvas canvas;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        canvas = GetComponent<Canvas>();

        currentPage = 0;
        UpdatePage();

        // ページ遷移ボタンにイベントをアタッチ
        leftButton.onClick.AddListener(() => ChangePage(false));
        rightButton.onClick.AddListener(() => ChangePage(true));

        // 戻るボタンにイベントをアタッチ
        backButton.onClick.AddListener(() => Close(this.GetCancellationTokenOnDestroy()));

        UpdatePage();
    }

    void Update()
    {
        if (!canvas.enabled) { return; }

        // ESCキーで閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close(this.GetCancellationTokenOnDestroy());
        }
    }

    public void Open()
    {
        currentPage = 0;
        UpdatePage();
        canvas.enabled = true;
    }

    public async void Close(CancellationToken ct = default)
    {
        try
        {
            await UniTask.DelayFrame(1, cancellationToken: ct); // pause画面も同時に抜けるのを防ぐためDelay
            canvas.enabled = false;
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }

    void UpdatePage()
    {
        // ボタンの表示を更新
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);

        if (currentPage == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        if (currentPage == (pages.Length - 1))
        {
            rightButton.gameObject.SetActive(false);
        }

        foreach (CanvasGroup page in pages)
        {
            if (page == pages[currentPage])
            {
                page.alpha = 1; // 現ページを表示
            }
            else
            {
                page.alpha = 0;
            }
        } 
    }

    void ChangePage(bool isTurnNextPage)
    {       
        // ページ数を更新
        if (isTurnNextPage)
        {
            currentPage = Mathf.Clamp((currentPage + 1), 0, (pages.Length - 1));
        }
        else
        {
            currentPage = Mathf.Clamp((currentPage - 1), 0, (pages.Length -1));
        }

        UpdatePage();
    }
}
