using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    public GameObject actionButtonPrefab; // Action butonlar�n�n prefab'�
    public int numButtons = 4; // Action butonlar�n�n say�s�
    public Sprite[] buttonIcons; // Action butonlar�n�n ikonlar�

    private Button[] actionButtons; // Olu�turulan action butonlar�n�n referanslar�

    private void Start()
    {
        CreateActionButtons();
    }

    private void CreateActionButtons()
    {
        actionButtons = new Button[numButtons];

        for (int i = 0; i < numButtons; i++)
        {
            GameObject buttonObj = Instantiate(actionButtonPrefab, transform);
            actionButtons[i] = buttonObj.GetComponent<Button>();

            // �konu ayarla
            Image buttonImage = buttonObj.GetComponent<Image>();
            buttonImage.sprite = buttonIcons[i % buttonIcons.Length];

            // Butona t�klama olay�n� ekle
            int buttonIndex = i; // Dikkat: De�i�kenin kopyas�n� al!
            actionButtons[i].onClick.AddListener(() =>
            {
                OnActionButtonClick(buttonIndex);
            });
        }
    }

    private void OnActionButtonClick(int buttonIndex)
    {
        // Action butonuna t�kland���nda yap�lacak i�lemler
        Debug.Log("Action Button " + buttonIndex + " clicked!");
    }
}