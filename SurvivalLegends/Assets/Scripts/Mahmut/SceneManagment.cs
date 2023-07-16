using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
       public void SinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer"); 
    }
       public void MultiPlayer()
    {
        SceneManager.LoadScene("MultiPlayer"); 
    }

        public void Ready()
    {
        SceneManager.LoadScene("Birlestirme");
    }

            public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

     public void Back()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
