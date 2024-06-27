using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// 2�̃I�u�W�F�N�g�̑I���E�����������s���A���Ԃ���ѕς��邱�ƂŊJ�����O
/// </summary>
/// <remarks>
/// inputKey��`�FinputIF[i]��j�Ԗڂɂ��� => inputKey[j] = i
/// </remarks>
public class SortLock : LockBase
{
    [SerializeReference] protected PasswordCA passwordCA = new PasswordCA();

    [Header("IF�I�����̈ʒu�E�ړ�����")]
    [SerializeField] Vector3 targetRelativePosition;    // IF�I�����̏����l�ɑ΂���ڕWPosition
    [SerializeField] Vector3 targetRelativeRotation;    // IF�I�����̏����l�ɑ΂���ڕWRotation
    [SerializeField] float selectionTime;               // IF�I�����̈ړ�����
    [SerializeField] float swapTime;                    // 1�ڂ�2�ڂ̑I��IF�̌������̈ړ�����

    int firstSelectedIndex = -1;    // 1�ڂ̑I��IF��index(��I���� -1)
    bool canMove = true;            // ����\��

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        for (int i = 0; i < inputIF.Count; i++)
        {
            inputIF[i].GetComponent<ClickInputIF>().index = i;  // �einputIF��index����U��
            passwordCA.inputKey.Add(i);                         // inputKey������
        }
    }

    /// <summary>
    /// inputIF�̑I���E�������s���AinputKey�̔F�؁E�������s��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
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
            // 1�ڂ�IF��I��
            if (firstSelectedIndex == -1)
            {
                // �I�����̈ʒu�ֈړ�
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                firstSelectedIndex = index;
            }

            // 1�ڂƓ����IF���ĂёI��(�I���̉���)
            else if (firstSelectedIndex == index)
            {
                // ���I�����̈ʒu�֖߂�
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                firstSelectedIndex = -1;
            }

            // 2�ڂ�IF��I��(1�ڂ̃I�u�W�F�N�g�ƈʒu������)
            else
            {
                // ����6�X�e�b�v�ɏ]���A2��IF������
                // [�X�e�b�v1] 2�ڂ�IF��I�����̈ʒu�ֈړ�
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                // [�X�e�b�v2] 1�ڂ�IF��2�ڂ�IF�̋������擾
                Vector3 difference = inputIF[index].transform.position - inputIF[firstSelectedIndex].transform.position;

                // [�X�e�b�v3] 2��IF���Փ˂��Ȃ��悤�ɁA2�ڂ�IF������Ɉړ�������
                await inputIF[index].transform.AddLocalPosition(targetRelativePosition, swapTime * 0.2f, linkedTokenSource.Token);

                // [�X�e�b�v4] 1�ڂ�IF��2�ڂ�IF������
                await UniTask.WhenAll(
                    inputIF[firstSelectedIndex].transform.AddLocalPosition(difference, swapTime * 0.6f, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalPosition(-1.0f * difference, swapTime * 0.6f, linkedTokenSource.Token));

                // [�X�e�b�v5] 2�ڂ�IF�ɂ��āA[�X�e�b�v3]�ł���Ɉړ��������������ɖ߂� 
                await inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, swapTime * 0.2f, linkedTokenSource.Token);

                // [�X�e�b�v6] 2��IF�𖢑I�����̈ʒu�֖߂� 
                await UniTask.WhenAll(
                    inputIF[index].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[index].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token),
                    inputIF[firstSelectedIndex].transform.AddLocalPosition(-1.0f * targetRelativePosition, selectionTime, linkedTokenSource.Token),
                    inputIF[firstSelectedIndex].transform.AddLocalRotation(-1.0f * targetRelativeRotation, selectionTime, linkedTokenSource.Token));

                // inputKey������
                int firstIndex = passwordCA.inputKey.IndexOf(firstSelectedIndex);
                int secondIndex = passwordCA.inputKey.IndexOf(index);
                passwordCA.inputKey.Swap(firstIndex, secondIndex);

                this.firstSelectedIndex = -1;

                // �F�؁E����
                if (passwordCA.Certification()) { Unlock(ct); }
            }

            canMove = true;
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(SortLock) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
        }
    }
}


