using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSelectShop : MonoBehaviour
{
    public GameObject[] skins;
    public int SelectedCharacter;
    public float spacing = 2f;
    public float testx;

    private void Awake()
    {
        skins[SelectedCharacter] = skins[0];
        SelectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        ArrangeCharacters();
    }

    public void ChangeNext()
    {

        SelectedCharacter++;
        if (SelectedCharacter >= skins.Length)
        {
            SelectedCharacter = 0;
        }

        ArrangeCharacters();
        PlayerPrefs.SetInt("SelectedCharacter", SelectedCharacter);
    }

    public void ChangePrevious()
    {

        SelectedCharacter--;
        if (SelectedCharacter < 0)
        {
            SelectedCharacter = skins.Length - 1;
        }

        ArrangeCharacters();
        PlayerPrefs.SetInt("SelectedCharacter", SelectedCharacter);
    }

    private void ArrangeCharacters()
    {
        float startX = testx; // Başlangıç noktasını değiştirin
        float startZ = -2.7f;

        Vector3[] targetPositions = new Vector3[skins.Length];

        for (int i = 0; i < skins.Length; i++)
        {
            int currentIndex = (i + SelectedCharacter) % skins.Length;
            float characterX = startX + i * spacing;
            float characterZ = startZ;

            if (i == 1)
            {
                characterZ = -5.7f;
            }

            if (i == 0)
            {
                characterX -= 2.3f; // Sadece 0. karakterin x pozisyonunu sola kaydırın
            }

            if (i == 2)
            {
                characterX -= 2;
            }

            Vector3 currentPosition = skins[currentIndex].transform.position;
            targetPositions[currentIndex] = new Vector3(characterX, currentPosition.y, characterZ);
        }

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].transform.position = targetPositions[i];
        }
    } 




}
