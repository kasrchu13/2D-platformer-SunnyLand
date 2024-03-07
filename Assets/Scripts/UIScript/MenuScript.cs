using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    #region Button Event
    public void GoToScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void leaveGame(){
        Application.Quit();
    }
    #endregion
}
