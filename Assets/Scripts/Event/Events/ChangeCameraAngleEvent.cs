using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCameraAngleEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(ChangeCameraAngleEvent) + " >")]
    [Space(10)]

    [SerializeField] CameraAngle.AngleKey destinationAngle;
    [SerializeField] float delayTime;

    /// <summary>
    /// �C�x���g�����s
    /// �J�����A���O����ω�������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);
            CameraManagaer.instance.ChangeCameraAngle(destinationAngle);
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(ChangeCameraAngleEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}
