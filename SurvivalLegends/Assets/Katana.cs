using UnityEngine;

public class Katana : MonoBehaviour
{
    public int damageAmount = 50; // Hasar miktar�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman")) // 'Dusman' tagine sahip nesneyle �arp��ma kontrol�
        {
            // E�er 'Dusman' tagine sahip nesneyle �arp���ld�ysa, hasar verme i�lemini ger�ekle�tir
            Dusman dusman = other.GetComponent<Dusman>();
            if (dusman != null)
            {
                dusman.HasarAl(damageAmount);
            }
        }
    }
}