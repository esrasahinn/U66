using UnityEngine;

public class Buton3 : MonoBehaviour
{
    //private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private ArcherPlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referansý eklendi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = ArcherPlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referansý alýndý
    }
    public void ButonTiklama()
    {
        ArcherPlayerBehaviour.GetInstance().PerformLeftShiftAction();
        controller.HidePopup();
        //controller.ResumeGame(); // Oyunu devam ettir
        Debug.Log("Karakterin caný dolduruldu.");
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour sýnýfýndaki PlayerHeal metodunu çaðýr
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }
}