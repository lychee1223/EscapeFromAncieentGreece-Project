using UnityEngine;

/// <summary>
/// サーチモードでインスタンス化されるオブジェクト
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
        // ドラッグでオブジェクトを回転させる
        if (Input.GetMouseButton(0) && Time.timeScale != 0.0f)
        {
            float dx = Input.GetAxis("Mouse X") * rotationalSpeed * -1.0f;
            float dy = Input.GetAxis("Mouse Y") * rotationalSpeed;

            transform.Rotate(Camera.main.transform.up, dx, Space.World);
            transform.Rotate(Camera.main.transform.right, dy, Space.World);
        }

        // SEの音量を変更
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
