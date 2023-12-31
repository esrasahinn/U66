using UnityEngine;
using UnityEngine.UI;

public class KatanaSpine : MonoBehaviour
{
    public KeyCode abilityKeyCode = KeyCode.E; // Yetenek tu�u
    [SerializeField] public NinjaInventoryComponent ninjaInventory; // NinjaInventoryComponent bile�eni
    AudioSource audiosource;
    private Button abilityButton;

    private void Start()
    {
        abilityButton = GetComponent<Button>();
        abilityButton.onClick.AddListener(ActivateAbility);
        audiosource = GetComponent<AudioSource>();
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
            audiosource.Play();
        }
    }
}