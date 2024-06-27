using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <remark>
/// main Camera�Ɏ��t����
/// </remark>
public class CameraManagaer : MonoBehaviour
{
    public static CameraManagaer instance;  // �V���O���g��

    CameraAngleDB cameraAngleDB;
    [field: SerializeField] public CameraAngle.AngleKey currentAngle { get; private set; }

    [Header("AngleChangeButton UI")]
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text currentAngleName;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        cameraAngleDB = DBManager.instance.angleDB; // DB�擾
        ChangeCameraAngle(currentAngle);            // �A���O����������

        // �e�A���O���ύX�{�^����ChangeCameraAngle()���A�^�b�`
        leftButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).leftAngle));
        rightButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).rightAngle));
        backButton.onClick.AddListener(() => ChangeCameraAngle(cameraAngleDB.Get(currentAngle).backAngle));
    }

    /// <summary>
    /// �J�����A���O����ω�������
    /// </summary>
    /// <param name="destinationAngle">�ύX��J�����A���O����key</param>
    public void ChangeCameraAngle(CameraAngle.AngleKey destinationAngle)
    {
        if (destinationAngle == CameraAngle.AngleKey.NONE) { return; }

        // DB����A���O�����擾
        CameraAngle destinationAngleData = cameraAngleDB.Get(destinationAngle);
        if (destinationAngleData == null) { return; }

        // �J�����ړ�
        transform.position = destinationAngleData.position;
        transform.rotation = Quaternion.Euler(destinationAngleData.rotation);

        // UI�X�V
        currentAngleName.text = destinationAngleData.angleName;
        UpdateButton(leftButton, destinationAngleData.leftAngle);
        UpdateButton(rightButton, destinationAngleData.rightAngle);
        UpdateButton(backButton, destinationAngleData.backAngle);

        currentAngle = destinationAngle;
    }

    /// <summary>
    /// �ړ���J�����A���O���� NONE �Ȃ�΃A���O���ύX�{�^�����\���ɂ���
    /// </summary>
    /// <param name="button"> �\����؂�ւ���{�^�� </param>
    /// <param name="angle"> �{�^���������̕ύX��J�����A���O����key </param>
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
