using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int XP;
    public float[] position;

    public PlayerData(PlayerScripts player)
    {
        level = player.level;
        health = player.health;
        XP = player.XP;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}