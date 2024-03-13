using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _score;

    private void Update() {
        ShowStat();
    }
    private void ShowStat(){

        var time = GameManager.Instance.GlobalTimer;
        _time.text = "Time" + (time/60).ToString("0") + ":" + (time % 60).ToString("0");

        var health = GameManager.Instance.PlayerHealth;
        if(health < 0) health = 0;
        _health.text = "x" + health.ToString();

        var score = GameManager.Instance.PlayerScore;
        _score.text = score.ToString() + "/" + GameManager.Instance.MaxScore;

    }
}
