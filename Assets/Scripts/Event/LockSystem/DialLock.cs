using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// 回転する各ダイアルが示すシンボルが正解の組み合わせならば開く錠前
/// </summary>
/// <remarks>
/// inputKey定義：inputIF[i]がj番目のシンボルを示す => inputKey[i] = j
/// </remarks>
public class DialLock : LockBase
{
    [SerializeReference] protected PasswordCA passwordCA = new PasswordCA();
    [SerializeField] int symbolNum;     // 各ダイヤルのシンボル数

    [Header("IFの回転軸・回転時間")]
    [SerializeField] bool axisLocalX;
    [SerializeField] bool axisLocalY;
    [SerializeField] bool axisLocalZ;
    [SerializeField] float rotateTime;

    bool canMove = true;    // 動作可能か

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        for (int i = 0; i < inputIF.Count; i++)
        {
            inputIF[i].GetComponent<ClickInputIF>().index = i;  // 各inputIFにindex割り振り
            passwordCA.inputKey.Add(0);                         // inputKey初期化
        }
    }

    /// <summary>
    /// inputIFの回転を行い、inputKeyの認証・解錠を行う
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public override async void Input(int index, CancellationToken ct = default)
    {
        if (!canMove) { return; }
        if (inputIF[index] == null) { return; }

        canMove = false;

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            this.GetCancellationTokenOnDestroy(),
            inputIF[index].GetCancellationTokenOnDestroy(),
            ct);

        // inputIFを回転
        float rotX = (360 / symbolNum) * System.Convert.ToInt32(axisLocalX);
        float rotY = (360 / symbolNum) * System.Convert.ToInt32(axisLocalY);
        float rotZ = (360 / symbolNum) * System.Convert.ToInt32(axisLocalZ);

        try
        {
            await inputIF[index].transform.AddLocalRotation(new Vector3(rotX, rotY, rotZ), rotateTime, linkedTokenSource.Token);

            // inputKeyを更新
            passwordCA.inputKey[index]++;
            passwordCA.inputKey[index] %= symbolNum;    // [0, symbolNum) の区間でループ

            // 認証
            if (passwordCA.Certification()) { Unlock(ct); }

            canMove = true;
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(DialLock) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
            return;
        }
    }
}


