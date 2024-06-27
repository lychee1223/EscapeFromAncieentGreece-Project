using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// ��]����e�_�C�A���������V���{���������̑g�ݍ��킹�Ȃ�ΊJ�����O
/// </summary>
/// <remarks>
/// inputKey��`�FinputIF[i]��j�Ԗڂ̃V���{�������� => inputKey[i] = j
/// </remarks>
public class DialLock : LockBase
{
    [SerializeReference] protected PasswordCA passwordCA = new PasswordCA();
    [SerializeField] int symbolNum;     // �e�_�C�����̃V���{����

    [Header("IF�̉�]���E��]����")]
    [SerializeField] bool axisLocalX;
    [SerializeField] bool axisLocalY;
    [SerializeField] bool axisLocalZ;
    [SerializeField] float rotateTime;

    bool canMove = true;    // ����\��

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        for (int i = 0; i < inputIF.Count; i++)
        {
            inputIF[i].GetComponent<ClickInputIF>().index = i;  // �einputIF��index����U��
            passwordCA.inputKey.Add(0);                         // inputKey������
        }
    }

    /// <summary>
    /// inputIF�̉�]���s���AinputKey�̔F�؁E�������s��
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

        // inputIF����]
        float rotX = (360 / symbolNum) * System.Convert.ToInt32(axisLocalX);
        float rotY = (360 / symbolNum) * System.Convert.ToInt32(axisLocalY);
        float rotZ = (360 / symbolNum) * System.Convert.ToInt32(axisLocalZ);

        try
        {
            await inputIF[index].transform.AddLocalRotation(new Vector3(rotX, rotY, rotZ), rotateTime, linkedTokenSource.Token);

            // inputKey���X�V
            passwordCA.inputKey[index]++;
            passwordCA.inputKey[index] %= symbolNum;    // [0, symbolNum) �̋�ԂŃ��[�v

            // �F��
            if (passwordCA.Certification()) { Unlock(ct); }

            canMove = true;
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(DialLock) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}


