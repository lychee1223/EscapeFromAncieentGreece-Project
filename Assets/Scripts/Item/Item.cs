using System;
using UnityEngine;

[Serializable]
public class Item
{
    public ItemKey key;

    [Header("Value")]
    public String name;
    [MultilineAttribute] public String explaination;
    public Sprite icon;
    public GameObject entity;

    /// <summary>
    /// Item‚Ìkeyˆê——
    /// </summary>
    public enum ItemKey
    {
        NONE,
        AMPHORA,
        AMPHORA_WITH_SCYTALE,
        AMPHORA_WITH_OIL,
        HOPLON,
        HOPLON_WITH_PAPER,
        SCYTALE,
        PAPER,
        DECORDED_SCYTALE,

        BEEF,
        TORCH,
        LIGHTED_TORCH,
        LADDER,
        HELMET,

        KOPIS,
        BOX,
        BOX_WITH_KEY,
        KEY,
    }
}

