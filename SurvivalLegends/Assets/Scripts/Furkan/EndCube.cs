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
            levelManager.EndLevel();
        }
    }

    public void SetCanEnterNextLevel(bool canEnter)
    {
        canEnterNextLevel = canEnter;
        if (canEnter)
        {
            FindObjectOfType<UIManager>().SetStatusText("Kapýya Koþ!");
        }
        else
        {
            FindObjectOfType<UIManager>().SetStatusText("Düþmanlarý Temizle!");
        }
    }

    public void EnemyDied()
    {
        // Bu fonksiyonu boþ býrakabilirsiniz veya baþka bir iþlem yapabilirsiniz
    }
}
