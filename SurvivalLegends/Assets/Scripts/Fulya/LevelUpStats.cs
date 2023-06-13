using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpStats : MonoBehaviour
{
    public int level = 1;
    public float experience { get; private set; }
    public Text lvlText;
    public Image expBarImage;
    public static int ExpNeedToLvlUp(int currentLevel)
    {
        if (currentLevel == 0)
            return 0;

        return (currentLevel * currentLevel + currentLevel) *5;
    }
    public void SetExperience(float exp)
    {
        experience += exp;
        float expNeeded = ExpNeedToLvlUp(level);
        float previousExperience = ExpNeedToLvlUp(level - 1);
        //Level up with Exp     


        if (experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExperience = ExpNeedToLvlUp(level - 1);
        }

        //Fill Exp Bar Image with Exp

        expBarImage.fillAmount = (experience - previousExperience) / (expNeeded - previousExperience);

        //Reset the Fillbar
        if (expBarImage.fillAmount == 1)
        {
            expBarImage.fillAmount = 0;
        }
    }
    public void LevelUp()
    {
        level++;
        lvlText.text = level.ToString("");
    }
}