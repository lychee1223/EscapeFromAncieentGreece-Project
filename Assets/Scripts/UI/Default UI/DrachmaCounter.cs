using TMPro;
using UnityEngine;

public class DrachmaCounter : MonoBehaviour
{
    public static DrachmaCounter instance;  //シングルトン

    [Header("所持金UI")]
    [SerializeField] TMP_Text fundageText;

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
        fundageText.text = "0";
    }

    /// <summary>
    /// UIを設定
    /// </summary>
    /// <param name="income">所持金</param>
    public void Set(int fundage)
    {
        fundageText.text = fundage.ToString();
    }
}
