using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class LockBase : MonoBehaviour
{
    [SerializeField] EventList unlockEventList;   // �������̎��s�C�x���g
    [SerializeField] protected List<GameObject> inputIF = new List<GameObject>();

    /// <summary>
    /// �������AunlokEvent�����s����
    /// </summary>
    protected void Unlock(CancellationToken ct = default)
    {
        unlockEventList.Play(ct);
    }

    /// <summary>
    /// inputIF[index]����̓��͂���������
    /// </summary>
    /// <param name="index">���͐M����LockSystem�ɑ��M����inputIF��index</param>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public abstract void Input(int index, CancellationToken ct = default);
}
