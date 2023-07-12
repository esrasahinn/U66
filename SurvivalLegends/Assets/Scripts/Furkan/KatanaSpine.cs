using UnityEngine;
using UnityEngine.UI;

public class KatanaSpine : MonoBehaviour
{
    public KeyCode abilityKeyCode = KeyCode.E; // Yetenek tu�u
    [SerializeField] public NinjaInventoryComponent ninjaInventory; // NinjaInventoryComponent bile�eni
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
            ninjaInventory.ActivateAbility(); // Yetene�i tetikle
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