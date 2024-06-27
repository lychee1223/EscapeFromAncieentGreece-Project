using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// inputIFが全てActiveならば開く錠前
/// </summary>
public class SetLock : LockBase
{
    /// <summary>
    /// inputKeyの認証・解錠を行う
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public override void Input(int index = 0, CancellationToken ct = default)
    {
        // 認証
        if (Certification()) { Unlock(ct); }
    }

    /// <summary>
    /// inputKeyのActive状態を確認し、認証を行う
    /// </summary>
    /// <returns>
    /// 全inputIFがActive => true
    /// 1つでもinputIFが非Active => false
    /// </returns>
    private bool Certification()
    {
        foreach (GameObject key in inputIF)
        {
            if (key == null) { continue; }
            if (!key.activeSelf) { return false; }
        }

        return true;
    }
}
