using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class ExtensionTransform
{
    /// <summary>
    /// �I�u�W�F�N�g��localPosition��targetRelativePosition��time�b�ňړ�������
    /// </summary>
    /// <param name="transform">Transform�^���g��</param>
    /// <param name="targetRelativePosition">�ړ���̑��Έʒu</param>
    /// <param name="time">�ړ�����</param>
    public static async UniTask AddLocalPosition(this Transform transform, Vector3 targetRelativePosition, float time, CancellationToken ct = default)
    {
        // �����l�ƖڕW�ʒu��localPosition���擾
        Vector3 fromPosition = transform.localPosition;
        Vector3 toPosition = fromPosition + targetRelativePosition;

        float currentTime = 0.0f;   // �o�ߎ���
        float rate;                 // �ړ�����

        // time �b�I�u�W�F�N�g�𓮂���������
        while (currentTime <= time)
        {
            // �o�ߎ��Ԃ��X�V
            currentTime += Time.deltaTime;

            // �ړ��������X�V
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            // �x�N�g�����Z
            transform.localPosition = Vector3.Lerp(fromPosition, toPosition, rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g��localRotation��targetRelativeRotation��time�b�ŉ�]������
    /// </summary>
    /// <param name="transform">Transform�^���g��</param>
    /// <param name="targetRelativeRotation">�ړ���̑��Έʒu</param>
    /// <param name="time">�ړ�����</param>
    public static async UniTask AddLocalRotation(this Transform transform, Vector3 targetRelativeRotation, float time, CancellationToken ct = default)
    {
        // �����l�ƖڕW�ʒu��localRotation���擾
        Quaternion fromRotation = transform.localRotation;
        Quaternion toRotation = fromRotation * Quaternion.Euler(targetRelativeRotation);

        float currentTime = 0.0f;   // �o�ߎ���
        float rate;                 // �ړ�����

        // time �b�I�u�W�F�N�g�𓮂���������
        while (currentTime <= time)
        {
            // �o�ߎ��Ԃ��X�V
            currentTime += Time.deltaTime;

            // �ړ��������X�V
            if (time == 0)
            {
                rate = 1.0f;
            }
            else
            {
                rate = Mathf.Clamp01(currentTime / time);
            }

            // �x�N�g�����Z
            transform.localRotation = Quaternion.Slerp(fromRotation, toRotation, rate);

            await UniTask.DelayFrame(1, cancellationToken: ct);
        }
    }
}
