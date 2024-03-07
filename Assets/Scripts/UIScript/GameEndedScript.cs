using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndedScript : MonoBehaviour
{
    public TextMeshProUGUI EndedLine;
    public void GameEnded(bool win){
        gameObject.SetActive(true);
        if(win) EndedLine.text = "Congratulation";
        else EndedLine.text = "Game Over";
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
