using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    [SerializeField] TMP_Text text;

    [Header("文字速度・表示時間")]
    [SerializeField] float textSpead = 0.05f;
    [SerializeField] float aliveTime = 5.0f;
    [SerializeField] float closeTime = 1.0f;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        text.maxVisibleCharacters = 0;
        canvas.alpha = 0.0f;
    }

    /// <summary>
    /// MessageWindowを表示
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async void Open(CancellationToken ct = default)
    {
        try
        {
            if (canvas.alpha != 0.0f) { return; }

            // canvasの表示
            canvas.alpha = 1.0f;

            // 最大表示文字数を更新し、テキストを1文字ずつ表示
            for (var i = 1; i <= text.text.Length; i++)
            {
                text.maxVisibleCharacters = i;
                await UniTask.Delay(System.TimeSpan.FromSeconds(textSpead), cancellationToken: ct);
            }

            // aliveTime秒後、Canvasを非表示にする
            await UniTask.Delay(System.TimeSpan.FromSeconds(aliveTime), cancellationToken: ct);
            await canvas.MakeTransparent(closeTime, ct);
        }
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(MessageWindow) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }
}
