using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// SetObjectLock��IF�ƂȂ�I�u�W�F�N�g�ɃA�^�b�`����
/// </summary>
public class ActiveInputIF : MonoBehaviour
{
    [SerializeField] SetLock lockSystem;

    /// <summary>
    /// �I�u�W�F�N�g��Active��ԂɂȂ������AlockSystem�ɓ��͐M���𑗂�
    /// </summary>
    void OnEnable()
    {
        lockSystem.Input(ct: this.GetCancellationTokenOnDestroy());
    }
}
