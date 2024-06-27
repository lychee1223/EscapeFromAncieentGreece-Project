using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveEvent : IEvent
{
    // List<IEvent>に追加されたインスタンスを明瞭にするため、ヘッダでクラス名を表示
    [Header("< " + nameof(SetActiveEvent) + " >")]
    [Space(10)]

    [SerializeField] GameObject[] enabledObject;
    [SerializeField] GameObject[] disabledObject;
    [SerializeField] float delayTime;

    /// <summary>
    /// イベントを実行
    /// ゲームオブジェクトを有効化・無効化
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);

            // enabledObjectを全て有効化
            foreach (GameObject gameObject in enabledObject)
            {
                if (gameObject == null) { continue; }
                gameObject.SetActive(true);
            }

            // disabledObjectを全て無効化
            foreach (GameObject gameObject in disabledObject)
            {
                if (gameObject == null) { continue; }
                gameObject.SetActive(false);
            }
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(SetActiveEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }
}
