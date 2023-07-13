using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string enemyTag = "Dusman";
    public EndCube endCube;
    private int enemyCount;
    private int deadEnemyCount;
    private UIManager uiManager;


    public GameObject victory;
    public GameObject defaited;

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemyCount = enemies.Length;
        deadEnemyCount = 0;

        // EndCube bile�enini bulma
        endCube = FindObjectOfType<EndCube>();

        // UIManager bile�enini bulma
        uiManager = FindObjectOfType<UIManager>();

        // Ba�lang��ta d��manlar� temizle mesaj�n� g�ncelleyin
        uiManager.SetStatusText("D��manlar� Temizle!");
    }

    public void EnemyDied()
    {
        deadEnemyCount++;

        if (deadEnemyCount >= enemyCount && enemyCount > 0)
        {
            // B�t�n d��manlar �ld�, son k�pten ge�i�e izin ver
            endCube.SetCanEnterNextLevel(true);
            // Kap�ya ko� mesaj�n� g�ncelle
            uiManager.SetStatusText("Kap�ya Ko�!");
        }
        else if (deadEnemyCount < enemyCount)
        {
            // Hen�z t�m d��manlar �lmediyse, mesaj� "D��manlar� Temizle!" olarak g�ncelle
            uiManager.SetStatusText("D��manlar� Temizle!");
        }
    }

    public void Victory() {


      victory.SetActive(true);

    }

     public void Defaited() {

        defaited.SetActive(false);


    }
    public void EndLevel()
    {
        
        SceneManager.LoadScene("LevelSelector");
    }
}
