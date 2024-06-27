using UnityEngine;

/// <summary>
/// �T�[�`���[�h�ŃC���X�^���X�������I�u�W�F�N�g
/// </summary>
public class SearchedEntity : MonoBehaviour
{
    static float rotationalSpeed = 20;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip startAudioClip;

    void Start()
    {
        float volume = SettingMenu.instance.seVolume;
        audioSource.PlayOneShot(startAudioClip, volume);
    }

    void Update()
    {
        // �h���b�O�ŃI�u�W�F�N�g����]������
        if (Input.GetMouseButton(0) && Time.timeScale != 0.0f)
        {
            float dx = Input.GetAxis("Mouse X") * rotationalSpeed * -1.0f;
            float dy = Input.GetAxis("Mouse Y") * rotationalSpeed;

            transform.Rotate(Camera.main.transform.up, dx, Space.World);
            transform.Rotate(Camera.main.transform.right, dy, Space.World);
        }

        // SE�̉��ʂ�ύX
        if (audioSource == null) { return; }

        if (!GameManager.instance.isPlayable || Time.timeScale == 0.0f)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
