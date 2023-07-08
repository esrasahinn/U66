using UnityEngine;

public class Buton3Rifle : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private PlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referansý eklendi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = PlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referansý alýndý
    }
    public void ButonTiklama()
    {
        PlayerBehaviour.GetInstance().PerformLeftShiftAction();
        controller.HidePopup();
        controller.ResumeGame(); // Oyunu devam ettir
        Debug.Log("Karakterin caný dolduruldu.");
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour sýnýfýndaki PlayerHeal metodunu çaðýr
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }
}