using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;   // シングルトン

    public CameraAngleDB angleDB;
    public ItemDB itemDB;
    public HintDB hintDB;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }
}
