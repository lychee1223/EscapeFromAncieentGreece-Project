using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour
{
    public static HintPanel instance;

    public Hint.Tag statusTag;
    HintDB hintDB;

    [SerializeField] CanvasGroup canvas;
    [SerializeField] Button button;
    [SerializeField] TMP_Text text;
    [SerializeField] float aliveTime;
    [SerializeField] float closeTime;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        instance = this;
        hintDB = DBManager.instance.hintDB; // DB取得
        button.onClick.AddListener(() => Open());
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

            // テキストを更新
            Hint hint = hintDB.Get();
            if (hint != null)
            {
                text.text = hint.hintText;
            }
            
            // canvasの表示
            canvas.alpha = 1.0f;

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
