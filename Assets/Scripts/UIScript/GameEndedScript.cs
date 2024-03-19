using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script should assign to GameEnded canvas
public class GameEndedScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += DisplayGameEndedScreen;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= DisplayGameEndedScreen;
    }

    public TextMeshProUGUI EndedLine;

    private void DisplayGameEndedScreen(GameState state)
    {
        gameObject.SetActive(state == GameState.Victory || state == GameState.Lose);

        if(state == GameState.Victory) EndedLine.text = "Congratulation";
        if(state == GameState.Lose) EndedLine.text = "Game Over";
    } 


    #region Button Event
    public void GoToScene(string sceneName){
        gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
    public void BacktoMenu(string menuScene){
        gameObject.SetActive(false);
        SceneManager.LoadScene(menuScene);
    }
    #endregion
}
