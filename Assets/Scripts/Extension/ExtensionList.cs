using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionList
{
    /// <summary>
    /// List<T> listに格納された要素の位置を変化させる
    /// </summary>
    /// <typeparam name="T">任意のListに格納する型</typeparam>
    /// <param name="list">List<T>型を拡張</param>
    /// <param name="oldIndex">移動前のindex</param>
    /// <param name="newIndex">移動後のindex</param>
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
    /// List<T> listに格納された2つの要素の位置を入れ替える
    /// </summary>
    /// <typeparam name="T">任意のListに格納する型</typeparam>
    /// <param name="list">List<T>型を拡張</param>
    /// <param name="i">入れ替える1つ目の要素のindex</param>
    /// <param name="j">入れ替える2つ目の要素のindex</param>
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
