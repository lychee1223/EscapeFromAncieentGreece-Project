using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <remark>
/// main Cameraに取り付ける
/// </remark>
public class CameraManagaer : MonoBehaviour
{
    public static CameraManagaer instance;  // シングルトン

    CameraAngleDB cameraAngleDB;
    [field: SerializeField] public CameraAngle.AngleKey currentAngle { get; private set; }

    [Header("AngleChangeButton UI")]
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text currentAngleName;

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        cameraAngleDB = DBManager.instance.angleDB; // DB取得
        ChangeCameraAngle(currentAngle);            // アングルを初期化

        // 各アングル変更ボタンにChangeCameraAngle()をアタッチ
        leftButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).leftAngle));
        rightButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).rightAngle));
        backButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).backAngle));
    }

    /// <summary>
    /// カメラアングルを変化させる
    /// </summary>
    /// <param name="destinationAngle">変更先カメラアングルのkey</param>
    public void ChangeCameraAngle(CameraAngle.AngleKey destinationAngle)
    {
        if (destinationAngle == CameraAngle.AngleKey.NONE) { return; }

        // DBからアングル情報取得
        CameraAngle destinationAngleData = cameraAngleDB.Get(destinationAngle);
        if (destinationAngleData == null) { return; }

        // カメラ移動
        transform.position = destinationAngleData.position;
        transform.rotation = Quaternion.Euler(destinationAngleData.rotation);

        // UI更新
        currentAngleName.text = destinationAngleData.angleName;
        UpdateButton(leftButton, destinationAngleData.leftAngle);
        UpdateButton(rightButton, destinationAngleData.rightAngle);
        UpdateButton(backButton, destinationAngleData.backAngle);

        currentAngle = destinationAngle;
    }

    /// <summary>
    /// 移動先カメラアングルが NONE ならばアングル変更ボタンを非表示にする
    /// </summary>
    /// <param name="button"> 表示を切り替えるボタン </param>
    /// <param name="angle"> ボタン押下時の変更先カメラアングルのkey </param>
    void UpdateButton(Button button, CameraAngle.AngleKey angle)
    {
        if (angle == CameraAngle.AngleKey.NONE)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
        }
    }
}
