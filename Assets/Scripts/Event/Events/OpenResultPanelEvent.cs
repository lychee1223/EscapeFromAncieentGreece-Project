using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenResultPanelEvent : IEvent
{
    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(OpenResultPanelEvent) + " >")]
    [Space(10)]

    [SerializeField] float delayTime;

    /// <summary>
    /// �C�x���g�����s
    /// ���b�Z�[�W�E�B���h�E��\��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Play(CancellationToken ct = default)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);
            ResultPanel.instance.Open(ct);
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(OpenResultPanelEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}