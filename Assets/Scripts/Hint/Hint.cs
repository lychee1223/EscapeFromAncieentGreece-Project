using System;
using UnityEngine;

[Serializable]
public class Hint
{
    public Tag statusTag;
    public Item.ItemKey[] requiredItems;
    [MultilineAttribute] public String hintText;

    public enum Tag
    {
        START,
        OPEN_GATE_DOOR,
        OPEN_OPISTHODOMUS_DOOR,
        SET_LADDER,
        SET_AMPHORA,
        OPEN_BUILDING03_DOOR,
        OPEN_PRONAOS_DOOR
    }
}
