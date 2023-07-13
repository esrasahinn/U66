using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCube : MonoBehaviour
{
    private LevelManager levelManager;

    private bool canEnterNextLevel = false;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canEnterNextLevel)
        {
          
            other.gameObject.SetActive(false); 
            levelManager.Victory();

        
        }
    }

    public void SetCanEnterNextLevel(bool canEnter)
    {
        canEnterNextLevel = canEnter;
        if (canEnter)
        {
            FindObjectOfType<UIManager>().SetStatusText("Kap�ya Ko�!");
        }
        else
        {
            FindObjectOfType<UIManager>().SetStatusText("D��manlar� Temizle!");
        }
    }

    public void EnemyDied()
    {
        // Bu fonksiyonu bo� b�rakabilirsiniz veya ba�ka bir i�lem yapabilirsiniz
    }
}
