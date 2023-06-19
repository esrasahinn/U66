using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    public int health;
    public int level;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Kayıt Başarılı!");
        Debug.Log(SaveSystem.LoadPlayer());
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;
         Debug.Log("Geri Yükleme Başarılı!");
    }
}
