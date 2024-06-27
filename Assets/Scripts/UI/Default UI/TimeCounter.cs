using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance; //シングルトン

    [Header("経過時間UI")]
    [SerializeField] TMP_Text timeText;

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
        timeText.text = "00:00";
    }

    /// <summary>
    /// UIを設定
    /// </summary>
    /// <param name="second">経過した秒数</param>
    public void Set(float second)
    {
        int minute = (int)(second / 60.0f);
        second -= minute * 60;

        // 経過時間を文字列に変換し表示
        timeText.text = minute.ToString("00") + ":" + ((int)second).ToString("00");
    }
}
