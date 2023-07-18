using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLogo : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
 
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f); 

        yield return LoadSceneAsync(9); 
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress * 2f);
            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }

        SceneManager.LoadScene(sceneId);
    }
}
