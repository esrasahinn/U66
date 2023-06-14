using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    public GameObject actionButtonPrefab; // Action butonlarýnýn prefab'ý
    public int numButtons = 4; // Action butonlarýnýn sayýsý
    public Sprite[] buttonIcons; // Action butonlarýnýn ikonlarý

    private Button[] actionButtons; // Oluþturulan action butonlarýnýn referanslarý

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

            // Ýkonu ayarla
            Image buttonImage = buttonObj.GetComponent<Image>();
            buttonImage.sprite = buttonIcons[i % buttonIcons.Length];

            // Butona týklama olayýný ekle
            int buttonIndex = i; // Dikkat: Deðiþkenin kopyasýný al!
            actionButtons[i].onClick.AddListener(() =>
            {
                OnActionButtonClick(buttonIndex);
            });
        }
    }

    private void OnActionButtonClick(int buttonIndex)
    {
        // Action butonuna týklandýðýnda yapýlacak iþlemler
        Debug.Log("Action Button " + buttonIndex + " clicked!");
    }
}