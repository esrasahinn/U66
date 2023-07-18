using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text statusText;

    public void SetStatusText(string text)
    {
        statusText.text = text;
    }
}