using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;   // �V���O���g��

    public CameraAngleDB angleDB;
    public ItemDB itemDB;
    public HintDB hintDB;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        instance = this;
    }
}
