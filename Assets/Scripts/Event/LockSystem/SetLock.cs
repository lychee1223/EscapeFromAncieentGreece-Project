using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// inputIF���S��Active�Ȃ�ΊJ�����O
/// </summary>
public class SetLock : LockBase
{
    /// <summary>
    /// inputKey�̔F�؁E�������s��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public override void Input(int index = 0, CancellationToken ct = default)
    {
        // �F��
        if (Certification()) { Unlock(ct); }
    }

    /// <summary>
    /// inputKey��Active��Ԃ��m�F���A�F�؂��s��
    /// </summary>
    /// <returns>
    /// �SinputIF��Active => true
    /// 1�ł�inputIF����Active => false
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
