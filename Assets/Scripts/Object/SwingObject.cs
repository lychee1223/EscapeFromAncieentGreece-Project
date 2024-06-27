using UnityEngine;

public class SwingObject : MonoBehaviour
{
    Vector3 initialPosition;
    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        initialPosition = transform.position;
    }

    /// <summary>
    /// �I�u�W�F�N�g��y���W��sin�g�ŐU��������
    /// </summary>
    void Update()
    {
        Vector3 position = default;
        position.y = amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = initialPosition + position;
    }
}
