using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelected : MonoBehaviour
{ 

       public int sceneId;
  
     public void LoadSceneID()
    {
        SceneManager.LoadScene("SinglePlayer"); 
    }

}
