using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraAngleDB : ScriptableObject
{
    public List<CameraAngle> angleList = new List<CameraAngle>();

    /// <summary>
    /// DB����f�[�^���擾�B�f�[�^���Ȃ��ꍇ��null��Ԃ�
    /// </summary>
    /// <param name="key">�擾����A���O����key</param>
    /// <returns>
    /// angleList��key�ɑΉ�����f�[�^�����݂���   => �f�[�^��Ԃ�
    ///                                 ���݂��Ȃ� => null��Ԃ�
    /// </returns>
    public CameraAngle Get(CameraAngle.AngleKey key)
    {
        foreach (CameraAngle angle in angleList)
        {
            if (angle.key == key) { return angle; }
        }

        return null;
    }
}
