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

        // EndCube bile�enini bulma
        endCube = FindObjectOfType<EndCube>();
    }

    public void EnemyDied()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            // B�t�n d��manlar �ld�, son k�pten ge�i�e izin ver
            endCube.SetCanEnterNextLevel(true);
        }
    }

    public void EndLevel()
    {
        // B�l�m�n bitmesini sa�layacak kodlar� buraya yazabilirsiniz.
        // �rne�in, yeni bir b�l�m y�klemek i�in:
        SceneManager.LoadScene("Birlestirme");
    }
}
