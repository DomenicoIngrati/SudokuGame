using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class ButtonSetting : MonoBehaviour
{
    public void ButtonClick(string matrix)
    {
        if(matrix=="9X9")
        SceneManager.LoadScene("GameScene9X9");
        else
        {
            SceneManager.LoadScene("GameScene16X16");
        }
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
