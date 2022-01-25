using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame(){
        Application.Quit();
    }
}
