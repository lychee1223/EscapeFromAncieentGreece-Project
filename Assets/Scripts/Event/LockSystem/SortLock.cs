using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// 2つのオブジェクトの選択・交換を順次行い、順番を並び変えることで開く錠前
/// </summary>
/// <remarks>
/// inputKey定義：inputIF[i]がj番目にある => inputKey[j] = i
/// </remarks>
public class SortLock : LockBase
{
    [SerializeReference] protected PasswordCA passwordCA = new PasswordCA();

    [Header("IF選択時の位置・移動時間")]
    [SerializeField] Vector3 targetRelativePosition;    // IF選択時の初期値に対する目標Position
    [SerializeField] Vector3 targetRelativeRotation;    // IF選択時の初期値に対する目標Rotation
    [SerializeField] float selectionTime;               // IF選択時の移動時間
    [SerializeField] float swapTime;                    // 1つ目と2つ目の選択中IFの交換時の移動時間

    int firstSelectedIndex = -1;    // 1つ目の選択中IFのindex(非選択時 -1)
    bool canMove = true;            // 動作可能か

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        for (int i = 0; i < inputIF.Count; i++)
        {
            inputIF[i].GetComponent<ClickInputIF>().index = i;  // 各inputIFにindex割り振り
            passwordCA.inputKey.Add(i);                         // inputKey初期化
        }
    }

    /// <summary>
    /// inputIFの選択・交換を行い、inputKeyの認証・解錠を行う
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

        try
        {
            // 1つ目のIFを選択
            if (firstSelectedIndex == -1)
            {
                // 選択時の位置へ移動
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                firstSelectedIndex = index;
            }

            // 1つ目と同一のIFを再び選択(選択の解除)
            else if (firstSelectedIndex == index)
            {
                // 未選択時の位置へ戻す
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                firstSelectedIndex = -1;
            }

            // 2つ目のIFを選択(1つ目のオブジェクトと位置を交換)
            else
            {
                // 次の6ステップに従い、2つのIFを交換
                // [ステップ1] 2つ目のIFを選択時の位置へ移動
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                // [ステップ2] 1つ目のIFと2つ目のIFの距離を取得
                Vector3 difference = inputIF[index].transform.position - inputIF[firstSelectedIndex].transform.position;

                // [ステップ3] 2つのIFが衝突しないように、2つ目のIFをさらに移動させる
                await inputIF[index].transform.AddLocalPosition(targetRelativePosition, swapTime * 0.2f, linkedTokenSource.Token);

                // [ステップ4] 1つ目のIFと2つ目のIFを交換
                await UniTask.WhenAll(
                    inputIF[firstSelectedIndex].transform.AddLocalPosition(difference, swapTime * 0.6f, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalPosition(-1.0f * difference, swapTime * 0.6f, linkedTokenSource.Token));

                // [ステップ5] 2つ目のIFについて、[ステップ3]でさらに移動させた分を元に戻す 
                await inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, swapTime * 0.2f, linkedTokenSource.Token);

                // [ステップ6] 2つのIFを未選択時の位置へ戻す 
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token),
                    inputIF[firstSelectedIndex].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[firstSelectedIndex].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                // inputKeyを交換
                int firstIndex = passwordCA.inputKey.IndexOf(firstSelectedIndex);
                int secondIndex = passwordCA.inputKey.IndexOf(index);
                passwordCA.inputKey.Swap(firstIndex, secondIndex);

                this.firstSelectedIndex = -1;

                // 認証・解錠
                if (passwordCA.Certification()) { Unlock(ct); }
            }

            canMove = true;
        }
        // UniTaskのキャンセルをcatch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(SortLock) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // タイトルに戻る
        }
    }
}


