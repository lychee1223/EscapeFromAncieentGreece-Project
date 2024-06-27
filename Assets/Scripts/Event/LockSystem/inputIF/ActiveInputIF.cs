using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// SetObjectLockのIFとなるオブジェクトにアタッチする
/// </summary>
public class ActiveInputIF : MonoBehaviour
{
    [SerializeField] SetLock lockSystem;

    /// <summary>
    /// オブジェクトがActive状態になった時、lockSystemに入力信号を送る
    /// </summary>
    void OnEnable()
    {
        lockSystem.Input(ct: this.GetCancellationTokenOnDestroy());
    }
}
