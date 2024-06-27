using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �����I�Ɍo�R�n�_�փI�u�W�F�N�g���ړ������A�n�_A(�����l)����n�_B�ɃI�u�W�F�N�g���ړ�������C�x���g
/// </summary>
public class MoveObjectEvent : IEvent
{
    /// <summary>
    /// �o�R�n�_�̑��Έʒu�ƈړ����Ԃ��i�[����N���X
    /// </summary>
    [System.Serializable]
    class ViaPoint
    {
        public Vector3 targetRelativePosition;  // �o�R�n�_�̑���Position
        public Vector3 targetRelativeRotation;  // �o�R�n�_�̑���Rotation
        public float moveTime;                  // �ړ�����
    }

    // List<IEvent>�ɒǉ����ꂽ�C���X�^���X�𖾗Ăɂ��邽�߁A�w�b�_�ŃN���X����\��
    [Header("< " + nameof(MoveObjectEvent) + " >")]
    [Space(10)]

    [SerializeField] GameObject movedObject;
    [SerializeField] List<ViaPoint> viaPoint = new List<ViaPoint>();   // �o�R�n�_ �n�_A:moveProcess[0] �n�_B:moveProcess[last]
    [SerializeField] bool canReturn;     // �n�_B����n�_A�ɖ߂邱�Ƃ��\��

    bool hasMoved = false;  // �n�_B�ɂ��邩�ۂ�
    bool canMove = true;    // ����\��    

    /// <summary>
    /// �C�x���g�����s
    /// movedObject��moveTime�Œn�_A�A�n�_B�Ԃ��ړ�������
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Play(CancellationToken ct = default)
    {
        if (!canMove) { return; }
        if (movedObject == null) { return; }

        canMove = false;

        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            movedObject.GetCancellationTokenOnDestroy(),
            ct);

        try
        {

            // �n�_B -> �n�_A �ֈړ�
            if (hasMoved)
            {
                if (!canReturn) { return; }

                // moveProcess���t���ɒH��AmovedObject�𒀎��I�Ɉړ�������
                viaPoint.Reverse();
                foreach (ViaPoint process in viaPoint)
                {
                    await UniTask.WhenAll(
                        movedObject.transform.AddLocalPosition(-1.0f * process.targetRelativePosition, process.moveTime, linkedTokenSource.Token),
                        movedObject.transform.AddLocalRotation(-1.0f * process.targetRelativeRotation, process.moveTime, linkedTokenSource.Token));
                }
                viaPoint.Reverse();
                hasMoved = false;
            }

            // �n�_A -> �n�_B �ֈړ�
            else
            {
                // moveProcess�����ɒH��AmovedObject�𒀎��I�Ɉړ�������
                foreach (ViaPoint process in viaPoint)
                {
                    await UniTask.WhenAll(
                        movedObject.transform.AddLocalPosition(process.targetRelativePosition, process.moveTime, linkedTokenSource.Token),
                        movedObject.transform.AddLocalRotation(process.targetRelativeRotation, process.moveTime, linkedTokenSource.Token));
                }
                hasMoved = true;
            }
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(MoveObjectEvent) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }

        canMove = true;
    }
}
