using UnityEngine;

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
    }
}
