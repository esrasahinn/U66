using UnityEngine;
using UnityEngine.UI;

public class KatanaSpine : MonoBehaviour
{
    public KeyCode abilityKeyCode = KeyCode.E; // Yetenek tuþu
    [SerializeField] public NinjaInventoryComponent ninjaInventory; // NinjaInventoryComponent bileþeni
    public AudioClip atesSesi;
    private Button abilityButton;

    private void Start()
    {
        abilityButton = GetComponent<Button>();
        abilityButton.onClick.AddListener(ActivateAbility);
    }

    private void Update()
    {
        if (Input.GetKeyDown(abilityKeyCode))
        {
            ninjaInventory.ActivateAbility(); // Yeteneði tetikle
        }
    }

    private void ActivateAbility()
    {
        if (!ninjaInventory.isInventoryActive && ninjaInventory.canUseAbility)
        {
            ninjaInventory.ToggleInventory();
            AudioSource.PlayClipAtPoint(atesSesi, transform.position);
        }
    }
}