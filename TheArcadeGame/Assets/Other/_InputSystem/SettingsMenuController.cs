using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SettingsMenuController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider first_selected;

    [Header("Input Action References")]
    [SerializeField] private InputActionReference toggle_menu_action;
    [SerializeField] private InputActionAsset input_actions_asset;

    [Header("Action Map Names")]
    [SerializeField] private string ui_action_map_name = "UI";
    [SerializeField] private string gameplay_action_map_name = "Gameplay";

    private bool is_navigating_menu = false;

    private void OnEnable()
    {
        if (toggle_menu_action != null)
        {
            toggle_menu_action.action.Enable();
            toggle_menu_action.action.performed += OnToggleMenuFocus;
        }
    }

    private void OnDisable()
    {
        if (toggle_menu_action != null)
        {
            toggle_menu_action.action.performed -= OnToggleMenuFocus;
            toggle_menu_action.action.Disable();
        }
    }

    private void OnToggleMenuFocus(InputAction.CallbackContext context)
    {
        is_navigating_menu = !is_navigating_menu;

        if (is_navigating_menu)
        {
            // Enable UI inputs, disable gameplay controls
            input_actions_asset.FindActionMap(gameplay_action_map_name)?.Disable();
            input_actions_asset.FindActionMap(ui_action_map_name)?.Enable();

            // Focus first slider for controller navigation
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(first_selected.gameObject);
        }
        else
        {
            // Clear UI focus and return control to gameplay
            EventSystem.current.SetSelectedGameObject(null);

            input_actions_asset.FindActionMap(ui_action_map_name)?.Disable();
            input_actions_asset.FindActionMap(gameplay_action_map_name)?.Enable();
        }
    }
}