using UnityEngine;

public class Fire : MonoBehaviour
{
    void Update()
    {
        // 常に上を向くようにする
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
