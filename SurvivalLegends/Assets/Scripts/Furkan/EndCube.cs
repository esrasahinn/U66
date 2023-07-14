using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCube : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerScripts playerScripts;

    private bool canEnterNextLevel = false;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerScripts = FindObjectOfType<PlayerScripts>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canEnterNextLevel)
        {
          
            other.gameObject.SetActive(false); 
            levelManager.Victory();
            playerScripts.LoadPlayer();


        
        }
    }

    public void SetCanEnterNextLevel(bool canEnter)
    {
        canEnterNextLevel = canEnter;
        if (canEnter)
        {
            FindObjectOfType<UIManager>().SetStatusText("Enemies cleared. Find the exit!");
        }
        else
        {
            FindObjectOfType<UIManager>().SetStatusText("Kill the Enemies!");
        }
    }

    public void EnemyDied()
    {
        // Bu fonksiyonu bo� b�rakabilirsiniz veya ba�ka bir i�lem yapabilirsiniz
    }
}
