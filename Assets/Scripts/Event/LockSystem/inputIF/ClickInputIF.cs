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
    /// lockSystem�ɓ��͐M���𑗂�
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public void Input(CancellationToken ct = default)
    {
        lockSystem.Input(index, ct);
    }
}
