using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool isActive = false; // Tuzak aktif mi?
    [SerializeField] int hasar = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true; // Tuzak oyuncuyla temas halinde aktif olur
            DealDamage(); // �lk anda da hasar verme fonksiyonunu �a��r�r
            InvokeRepeating("DealDamage", 1f, 1f); // 1 saniyede bir hasar verme fonksiyonunu �a��r�r
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false; // Tuzak oyuncuyla temas sona erdi�inde pasif hale gelir
            CancelInvoke("DealDamage"); // Hasar verme fonksiyonunu durdurur
        }
    }

    private void DealDamage()
    {
        if (isActive)
        {
            ArcherPlayerBehaviour Arcplayer = FindObjectOfType<ArcherPlayerBehaviour>();
            if (Arcplayer != null)
            {
                Arcplayer.PlayerTakeDmg(hasar);
            }
        }

        if (isActive)
        {
            PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
            if (player != null)
            {
                player.PlayerTakeDmg(hasar);
            }
        }
    }
}
