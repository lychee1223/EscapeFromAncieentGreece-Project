using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraAngleDB : ScriptableObject
{
    public List<CameraAngle> angleList = new List<CameraAngle>();

    /// <summary>
    /// DBからデータを取得。データがない場合はnullを返す
    /// </summary>
    /// <param name="key">取得するアングルのkey</param>
    /// <returns>
    /// angleListにkeyに対応するデータが存在する   => データを返す
    ///                                 存在しない => nullを返す
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
