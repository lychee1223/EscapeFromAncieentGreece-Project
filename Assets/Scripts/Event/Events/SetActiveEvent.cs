using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(SetActiveEvent) + " >")]
    [Space(10)]

    [SerializeField] GameObject[] enabledObject;
    [SerializeField] GameObject[] disabledObject;
    [SerializeField] float delayTime;

    /// <summary>
    /// �C�x���g�����s
    /// �Q�[���I�u�W�F�N�g��L�����E������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);

            // enabledObject��S�ėL����
            foreach (GameObject gameObject in enabledObject)
            {
                if (gameObject == null) { continue; }
                gameObject.SetActive(true);
            }

            // disabledObject��S�Ė�����
            foreach (GameObject gameObject in disabledObject)
            {
                if (gameObject == null) { continue; }
                gameObject.SetActive(false);
            }
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(SetActiveEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}
