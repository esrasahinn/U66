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
    private int previousLevel; // Önceki seviyeyi saklamak için değişken


    private void Start()
    {
         
        LoadPlayer();
    }

  public void SavePlayer()
{
    previousLevel = level; // Önceki seviyeyi sakla

    level++;

    // Diğer kaydetme işlemleri
    SaveSystem.SavePlayer(this);
    SceneManager.LoadScene(4);
    Debug.Log("Kayıt Başarılı!");
    Debug.Log(SaveSystem.LoadPlayer());
}

public void LoadPreviousLevel()
{
    level = previousLevel; 
    SceneManager.LoadScene(previousLevel); 
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
