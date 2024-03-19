using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script should assign to PauseMenu canvas
public class PauseMenuScript : MonoBehaviour
{
    private void Awake() 
    {
        GameManager.OnGameStateChanged += DisplayPauseMenu;
    }
    private void OnDestroy() 
    {
        GameManager.OnGameStateChanged -= DisplayPauseMenu;
    }
    private void DisplayPauseMenu(GameState state)
    {
        gameObject.SetActive(state == GameState.Paused);
    }

    #region Button Event
    public void Resume(){
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
    public void BacktoMenu(string menuScene){
        gameObject.SetActive(false);
        SceneManager.LoadScene(menuScene);
    }
    #endregion

}
