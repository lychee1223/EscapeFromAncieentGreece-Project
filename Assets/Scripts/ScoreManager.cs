using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance; //ƒVƒ“ƒOƒ‹ƒgƒ“

    public int riskScore { get; private set; } = 0;
    public int drachmaScore { get; private set; } = 0;
    public int timeScore { get; private set; } = 0;
    public int totalScore { get; private set; } = 0;

    [SerializeField] float riskScoreWeight;
    [SerializeField] float timeScoreWeight;
    [SerializeField] float timeScoreBias;
    [SerializeField] float drachmaScoreWeight;
    [field: SerializeField] public float clearBonus { get; private set; }

    void Start()
    {
        instance = this;
    }

    public void CalculateScore(float riskRate, int fundage, float second, bool isClear)
    {
        riskScore = Mathf.RoundToInt(riskScoreWeight * (1.0f - riskRate));

        drachmaScore = Mathf.RoundToInt(drachmaScoreWeight * fundage);

        if (isClear)
        {
            timeScore = Mathf.RoundToInt(timeScoreWeight * (60.0f / second) + timeScoreBias);
        }
        else
        {
            timeScore = Mathf.RoundToInt(Mathf.Min(timeScoreBias, (timeScoreBias / timeScoreWeight) * second));
        }

        totalScore = riskScore + drachmaScore + timeScore;
        if (isClear)
        {
            totalScore = Mathf.RoundToInt(totalScore * clearBonus);
        }
    }
}
