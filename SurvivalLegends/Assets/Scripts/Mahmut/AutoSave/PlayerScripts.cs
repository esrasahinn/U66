using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerScripts : MonoBehaviour
{
    public int health;
    public int level;
    public int XP;
    public TMP_Text levelText;
    public TMP_Text levelText2;
  


    private void Start()
    {
         
        LoadPlayer();
    }

  public void SavePlayer()
{


    level++;

    // Diğer kaydetme işlemleri
    SaveSystem.SavePlayer(this);
    SceneManager.LoadScene(5);
    Debug.Log("Kayıt Başarılı!");
    Debug.Log(SaveSystem.LoadPlayer());
}



    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
       
        level = data.level;
        health = data.health;
        XP = data.XP;
         levelText.text = level.ToString();
        levelText2.text = level.ToString();
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;

        Debug.Log("Geri Yükleme Başarılı!");
    }
}
