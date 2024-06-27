using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ClickInputIF : MonoBehaviour
{
    [SerializeField] LockBase lockSystem;
    public int index { get; set; }

    /// <summary>
    /// lockSystemに入力信号を送る
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public void Input(CancellationToken ct = default)
    {
        lockSystem.Input(index, ct);
    }
}
