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
    public static HowToPage instance; // �V���O���g��

    [SerializeField] CanvasGroup[] pages;
    int currentPage = 0;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button backButton;
    
    Canvas canvas;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        canvas = GetComponent<Canvas>();

        currentPage = 0;
        UpdatePage();

        // �y�[�W�J�ڃ{�^���ɃC�x���g���A�^�b�`
        leftButton.onClick.AddListener(() => ChangePage(false));
        rightButton.onClick.AddListener(() => ChangePage(true));

        // �߂�{�^���ɃC�x���g���A�^�b�`
        backButton.onClick.AddListener(() => Close(this.GetCancellationTokenOnDestroy()));

        UpdatePage();
    }

    void Update()
    {
        if (!canvas.enabled) { return; }

        // ESC�L�[�ŕ���
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
            await UniTask.DelayFrame(1, cancellationToken: ct); // pause��ʂ������ɔ�����̂�h������Delay
            canvas.enabled = false;
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }

    void UpdatePage()
    {
        // �{�^���̕\�����X�V
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
                page.alpha = 1; // ���y�[�W��\��
            }
            else
            {
                page.alpha = 0;
            }
        } 
    }

    void ChangePage(bool isTurnNextPage)
    {       
        // �y�[�W�����X�V
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
