using UnityEngine;

public class Buton3Rifle : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private PlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referans� eklendi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = PlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referans� al�nd�
    }
    public void ButonTiklama()
    {
        PlayerBehaviour.GetInstance().PerformLeftShiftAction();
        controller.HidePopup();
        controller.ResumeGame(); // Oyunu devam ettir
        Debug.Log("Karakterin can� dolduruldu.");
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour s�n�f�ndaki PlayerHeal metodunu �a��r
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }
}