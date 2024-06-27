using UnityEngine;
using UnityEngine.UI;

public class RiskGauge : MonoBehaviour
{
    public static RiskGauge instance;    //シングルトン

    [Header("感染ゲージUI")]
    [SerializeField] Image fillArea;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        fillArea.fillAmount = 0.0f;
    }

    /// <summary>
    /// UIを設定
    /// </summary>
    /// <param name="rate">感染率</param>
    public void Set(float rate)
    {
        // ゲージの進捗を更新
        fillArea.fillAmount = rate;
    }
}
