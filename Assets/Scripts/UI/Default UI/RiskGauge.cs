using UnityEngine;
using UnityEngine.UI;

public class RiskGauge : MonoBehaviour
{
    public static RiskGauge instance;    //�V���O���g��

    [Header("�����Q�[�WUI")]
    [SerializeField] Image fillArea;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        fillArea.fillAmount = 0.0f;
    }

    /// <summary>
    /// UI��ݒ�
    /// </summary>
    /// <param name="rate">������</param>
    public void Set(float rate)
    {
        // �Q�[�W�̐i�����X�V
        fillArea.fillAmount = rate;
    }
}
