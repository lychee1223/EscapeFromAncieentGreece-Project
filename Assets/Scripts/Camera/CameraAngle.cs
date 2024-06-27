using System;
using UnityEngine;

[Serializable]
public class CameraAngle
{
    public AngleKey key;
    public String angleName;

    [Header("Value")]
    public Vector3 position;
    public Vector3 rotation;

    // 各アングル変更ボタン押下時の遷移先アングル
    public AngleKey leftAngle;
    public AngleKey rightAngle;
    public AngleKey backAngle;

    /// <summary>
    /// CameraAngleのkey一覧
    /// </summary>
    public enum AngleKey
    {
        NONE,
        AREA1_DEFAULT1,
        AREA1_DEFAULT2,
        AREA1_DEFAULT3,
        AREA1_DEFAULT4,
        AREA1_GATE_LOCK,
        AREA1_OFFERING,
        AREA1_ARTEMIS,

        AREA2_DEFAULT1,
        AREA2_DEFAULT2,
        AREA2_DEFAULT3,
        AREA2_ALTER,
        AREA2_OPISTHODOMUS_DOOR,
        AREA2_OPISTHODOMUS, 

        AREA3_DEFAULT1,
        AREA3_DEFAULT2,
        AREA3_FENCE,
        AREA3_PRONAOS_DOOR,
        AREA3_PRONAOS_LOCK,
        AREA3_PRONAOS,
        AREA3_ERECHTHEION_DOOR,
        AREA3_ERECHTHEION_LOCK,
        AREA3_ERECHTHEION,
        AREA3_CARYATID,
        AREA3_OLIVE,
        AREA3_BUILDING03,
    }
}
