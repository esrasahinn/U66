using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string enemyTag = "Dusman";
    public EndCube endCube;
    private int enemyCount;
    private int deadEnemyCount;
    private UIManager uiManager;

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemyCount = enemies.Length;
        deadEnemyCount = 0;

        // EndCube bileþenini bulma
        endCube = FindObjectOfType<EndCube>();

        // UIManager bileþenini bulma
        uiManager = FindObjectOfType<UIManager>();

        // Baþlangýçta düþmanlarý temizle mesajýný güncelleyin
        uiManager.SetStatusText("Düþmanlarý Temizle!");
    }

    public void EnemyDied()
    {
        deadEnemyCount++;

        if (deadEnemyCount >= enemyCount && enemyCount > 0)
        {
            // Bütün düþmanlar öldü, son küpten geçiþe izin ver
            endCube.SetCanEnterNextLevel(true);
            // Kapýya koþ mesajýný güncelle
            uiManager.SetStatusText("Kapýya Koþ!");
        }
        else if (deadEnemyCount < enemyCount)
        {
            // Henüz tüm düþmanlar ölmediyse, mesajý "Düþmanlarý Temizle!" olarak güncelle
            uiManager.SetStatusText("Düþmanlarý Temizle!");
        }
    }


    public void EndLevel()
    {
        // Bölümün bitmesini saðlayacak kodlarý buraya yazabilirsiniz.
        // Örneðin, yeni bir bölüm yüklemek için:
        SceneManager.LoadScene("Birlestirme");
    }
}
