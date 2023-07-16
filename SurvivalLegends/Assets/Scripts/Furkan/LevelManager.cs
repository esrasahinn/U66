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

        // EndCube bileşenini bulma
        endCube = FindObjectOfType<EndCube>();

        // UIManager bileşenini bulma
        uiManager = FindObjectOfType<UIManager>();

        // Başlangıçta düşmanları temizle mesajını güncelleyin
        uiManager.SetStatusText("Kill the Enemies!");
    }

    public void EnemyDied()
    {
        deadEnemyCount++;

        if (deadEnemyCount >= enemyCount && enemyCount > 0)
        {
            // Bütün düşmanlar öldü, son kaptan geçişe izin ver
            endCube.SetCanEnterNextLevel(true);
            // Kapıya koş mesajını güncelle
            uiManager.SetStatusText("Enemies cleared. Find the exit!");
        }
        else if (deadEnemyCount < enemyCount)
        {
            // Henüz tüm düşmanlar ölmediyse, mesajı "Düşmanları Temizle!" olarak güncelle
            uiManager.SetStatusText("Kill the Enemies!");
        }
    }

    public void Victory()
    {
        victory.SetActive(true);
    }

    public void Defaited()
    {
        defaited.SetActive(true);
    }

    public void EndLevel()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
