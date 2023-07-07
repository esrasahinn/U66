using UnityEngine;

public class Buton3 : MonoBehaviour
{
    //private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private ArcherPlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referans� eklendi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = ArcherPlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referans� al�nd�
    }
    public void ButonTiklama()
    {
        ArcherPlayerBehaviour.GetInstance().PerformLeftShiftAction();
        controller.HidePopup();
        //controller.ResumeGame(); // Oyunu devam ettir
        Debug.Log("Karakterin can� dolduruldu.");
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour s�n�f�ndaki PlayerHeal metodunu �a��r
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }
}