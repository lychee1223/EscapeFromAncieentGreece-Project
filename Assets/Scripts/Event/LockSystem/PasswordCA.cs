using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCA
{
    [SerializeField] List<int> answerKey = new List<int>();
    public List<int> inputKey { get; set; } = new List<int>();

    /// <summary>
    /// inputKeyがanswerKeyと一致するか確認し、認証を行う
    /// </summary>
    /// <returns>
    /// inputKey == answerKey => true
    /// inputKey != answerKey => false
    /// </returns>
    public bool Certification()
    {
        if (inputKey.Count != answerKey.Count) { return false; }

        for (int i = 0; i < answerKey.Count; i++)
        {
            if (inputKey[i] != answerKey[i]) { return false; }
        }

        return true;
    }
}
