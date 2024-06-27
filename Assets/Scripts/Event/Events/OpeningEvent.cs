using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class OpeningEvent : MonoBehaviour
{
    public static OpeningEvent instance;    // シングルトン

    [SerializeField] CanvasGroup prologueCanvas;
    [SerializeField] TMP_Text prologueText;
    [SerializeField] TMP_Text triangle;

    [SerializeField] float textSpead;
    bool shouldSkip;
    [MultilineAttribute]
    [SerializeField] string[] prologueTexts;

    [SerializeField] float closeTime;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldSkip = true;
        }
    }

    /// <summary>
    /// イベントを実行
    /// プロローグを表示する
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async UniTask Play(CancellationToken ct = default)
    {
        // プロローグのテキストを順に再生
        foreach (string text in prologueTexts)
        {
            prologueText.maxVisibleCharacters = 0;
            triangle.enabled = false;
            shouldSkip = false;

            prologueText.text = text;

            // 最大表示文字数を更新し、テキストを1文字ずつ表示
            for (var i = 1; i <= prologueText.text.Length; i++)
            {
                if (shouldSkip)
                {
                    prologueText.maxVisibleCharacters = prologueText.text.Length;
                    break;
                }

                prologueText.maxVisibleCharacters = i;
                await UniTask.Delay(System.TimeSpan.FromSeconds(textSpead), cancellationToken: ct);
            }

            // 左クリックが押下されるまで待機
            triangle.enabled = true;

            await UniTask.DelayFrame(1, cancellationToken: ct);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken:ct);
            await UniTask.DelayFrame(1, cancellationToken: ct);
        }

        // Canvasを非表示にする
        await prologueCanvas.MakeTransparent(closeTime, ct);
        prologueCanvas.gameObject.SetActive(false);
    }
}
