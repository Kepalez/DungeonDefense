using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGamePushed(){
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
