using UnityEngine;

public class SwingObject : MonoBehaviour
{
    Vector3 initialPosition;
    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        initialPosition = transform.position;
    }

    /// <summary>
    /// オブジェクトのy座標をsin波で振動させる
    /// </summary>
    void Update()
    {
        Vector3 position = default;
        position.y = amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = initialPosition + position;
    }
}
