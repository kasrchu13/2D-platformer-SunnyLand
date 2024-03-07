using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIScript : MonoBehaviour
{
    [SerializeField] private PlayerMain _stats;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _score;

    private void Update() {
        ShowStat();
    }
    private void ShowStat(){
        var time = _stats.Timer;
        _time.text = "Time" + (_stats.Timer/60).ToString("0") + ":" + (_stats.Timer % 60).ToString("0");

        var health = _stats.PlayerHealth;
        if(health < 0) health = 0;
        _health.text = "x" + health.ToString();


        var score = _stats.PlayerScore;
        _score.text = score.ToString() + "/" + _stats.MaxScore;

    }
}
