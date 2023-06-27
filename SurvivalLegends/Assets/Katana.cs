using UnityEngine;

public class Katana : MonoBehaviour
{
    public int damageAmount = 50; // Hasar miktarý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman")) // 'Dusman' tagine sahip nesneyle çarpýþma kontrolü
        {
            // Eðer 'Dusman' tagine sahip nesneyle çarpýþýldýysa, hasar verme iþlemini gerçekleþtir
            Dusman dusman = other.GetComponent<Dusman>();
            if (dusman != null)
            {
                dusman.HasarAl(damageAmount);
            }
        }
    }
}