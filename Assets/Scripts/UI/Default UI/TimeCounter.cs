using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance; //�V���O���g��

    [Header("�o�ߎ���UI")]
    [SerializeField] TMP_Text timeText;

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
        timeText.text = "00:00";
    }

    /// <summary>
    /// UI��ݒ�
    /// </summary>
    /// <param name="second">�o�߂����b��</param>
    public void Set(float second)
    {
        int minute = (int)(second / 60.0f);
        second -= minute * 60;

        // �o�ߎ��Ԃ𕶎���ɕϊ����\��
        timeText.text = minute.ToString("00") + ":" + ((int)second).ToString("00");
    }
}
