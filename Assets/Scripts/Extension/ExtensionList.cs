using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionList
{
    /// <summary>
    /// List<T> list�Ɋi�[���ꂽ�v�f�̈ʒu��ω�������
    /// </summary>
    /// <typeparam name="T">�C�ӂ�List�Ɋi�[����^</typeparam>
    /// <param name="list">List<T>�^���g��</param>
    /// <param name="oldIndex">�ړ��O��index</param>
    /// <param name="newIndex">�ړ����index</param>
    public static void ChangeOrder<T>(this List<T> list, int oldIndex, int newIndex)
    {
        if (oldIndex >= list.Count || newIndex >= list.Count)
        {
            throw new System.ArgumentOutOfRangeException(nameof(newIndex));
        }

        if (oldIndex == newIndex)
        {
            return;
        }

        T element = list[oldIndex];
        list.RemoveAt(oldIndex);

        if (newIndex > list.Count)
        {
            list.Add(element);
        }
        else
        {
            list.Insert(newIndex, element);
        }
    }

    /// <summary>
    /// List<T> list�Ɋi�[���ꂽ2�̗v�f�̈ʒu�����ւ���
    /// </summary>
    /// <typeparam name="T">�C�ӂ�List�Ɋi�[����^</typeparam>
    /// <param name="list">List<T>�^���g��</param>
    /// <param name="i">����ւ���1�ڂ̗v�f��index</param>
    /// <param name="j">����ւ���2�ڂ̗v�f��index</param>
    public static void Swap<T>(this List<T> list, int i, int j)
    {
        if (i >= list.Count || j >= list.Count)
        {
            throw new System.ArgumentOutOfRangeException(nameof(i));
        }

        if (i == j)
        {
            return;
        }

        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
