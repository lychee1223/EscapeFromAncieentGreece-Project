using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class LockBase : MonoBehaviour
{
    [SerializeField] EventList unlockEventList;   // 解錠時の実行イベント
    [SerializeField] protected List<GameObject> inputIF = new List<GameObject>();

    /// <summary>
    /// 解錠し、unlokEventを実行する
    /// </summary>
    protected void Unlock(CancellationToken ct = default)
    {
        unlockEventList.Play(ct);
    }

    /// <summary>
    /// inputIF[index]からの入力を処理する
    /// </summary>
    /// <param name="index">入力信号をLockSystemに送信するinputIFのindex</param>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public abstract void Input(int index, CancellationToken ct = default);
}
