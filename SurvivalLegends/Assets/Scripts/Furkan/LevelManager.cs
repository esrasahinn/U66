using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string enemyTag = "Dusman";
    public EndCube endCube;
    private int enemyCount;

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemyCount = enemies.Length;

        // EndCube bileþenini bulma
        endCube = FindObjectOfType<EndCube>();
    }

    public void EnemyDied()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            // Bütün düþmanlar öldü, son küpten geçiþe izin ver
            endCube.SetCanEnterNextLevel(true);
        }
    }

    public void EndLevel()
    {
        // Bölümün bitmesini saðlayacak kodlarý buraya yazabilirsiniz.
        // Örneðin, yeni bir bölüm yüklemek için:
        SceneManager.LoadScene("Birlestirme");
    }
}
