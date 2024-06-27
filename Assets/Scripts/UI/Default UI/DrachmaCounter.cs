using TMPro;
using UnityEngine;

public class DrachmaCounter : MonoBehaviour
{
    public static DrachmaCounter instance;  //�V���O���g��

    [Header("������UI")]
    [SerializeField] TMP_Text fundageText;

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
        fundageText.text = "0";
    }

    /// <summary>
    /// UI��ݒ�
    /// </summary>
    /// <param name="income">������</param>
    public void Set(int fundage)
    {
        fundageText.text = fundage.ToString();
    }
}
