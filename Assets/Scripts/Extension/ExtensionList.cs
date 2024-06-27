using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionList
{
    /// <summary>
    /// List<T> list‚ÉŠi”[‚³‚ê‚½—v‘f‚ÌˆÊ’u‚ð•Ï‰»‚³‚¹‚é
    /// </summary>
    /// <typeparam name="T">”CˆÓ‚ÌList‚ÉŠi”[‚·‚éŒ^</typeparam>
    /// <param name="list">List<T>Œ^‚ðŠg’£</param>
    /// <param name="oldIndex">ˆÚ“®‘O‚Ìindex</param>
    /// <param name="newIndex">ˆÚ“®Œã‚Ìindex</param>
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
    /// List<T> list‚ÉŠi”[‚³‚ê‚½2‚Â‚Ì—v‘f‚ÌˆÊ’u‚ð“ü‚ê‘Ö‚¦‚é
    /// </summary>
    /// <typeparam name="T">”CˆÓ‚ÌList‚ÉŠi”[‚·‚éŒ^</typeparam>
    /// <param name="list">List<T>Œ^‚ðŠg’£</param>
    /// <param name="i">“ü‚ê‘Ö‚¦‚é1‚Â–Ú‚Ì—v‘f‚Ìindex</param>
    /// <param name="j">“ü‚ê‘Ö‚¦‚é2‚Â–Ú‚Ì—v‘f‚Ìindex</param>
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
