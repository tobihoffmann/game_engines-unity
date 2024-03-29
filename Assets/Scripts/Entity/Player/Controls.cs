// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Entity/Player/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a16c2ac9-5582-4297-857c-8fa63c220faa"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""a341524c-8085-4eef-a077-1ea821491a16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""fb2527f2-d883-4b0d-bc68-41f2d7eff2f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""6b206025-de39-4765-b898-3f7db2fcf695"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WalkingVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b5ae3584-e4c0-42cb-8cd1-f9f1627ba301"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WalkingHorizontal"",
                    ""type"": ""Button"",
                    ""id"": ""e6f2e0db-cdce-47ab-ba84-178f5c977e90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""664d3df1-0110-44d7-9414-0e51862c3aee"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f66e7749-558a-42e7-8ad5-1831351490d5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32c5af7e-3b0a-4b48-af8e-8e567bd843f3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f8a1924b-cd00-49d4-ac43-469b5032bf62"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WalkingVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b5d5dcf4-63a0-4be5-b3df-01afbf5a0ac1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""WalkingVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""28fbc9c9-51ce-4145-802d-467e653d6cd1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""WalkingVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fb6d6b22-bc4b-4d44-8c2d-206be9e6b359"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""WalkingHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7d99dc49-b1c2-4b95-a60c-af7fca17377d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""WalkingHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7a0db755-4a34-4dd9-867a-9f9268dbf6ed"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""WalkingHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""14d0d2a8-de0c-4229-879b-14400f52be4f"",
            ""actions"": [
                {
                    ""name"": ""DropItem_01"",
                    ""type"": ""Button"",
                    ""id"": ""845f27e9-a293-4d88-babf-f84da76f94e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem_02"",
                    ""type"": ""Button"",
                    ""id"": ""62a75b30-9d54-428b-80e0-712fe3ac1d60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem_03"",
                    ""type"": ""Button"",
                    ""id"": ""42a8794e-f0e1-426e-92fc-b6c292e49c30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b84e8172-081f-444a-ab3c-0fed64386388"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""DropItem_01"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""284bbd41-1a94-4351-bef6-a0b38c6719ee"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""DropItem_02"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d85a7db5-ab52-4301-97ba-bf993dee2c3f"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboad and Mouse"",
                    ""action"": ""DropItem_03"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboad and Mouse"",
            ""bindingGroup"": ""Keyboad and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Melee = m_Player.FindAction("Melee", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_WalkingVertical = m_Player.FindAction("WalkingVertical", throwIfNotFound: true);
        m_Player_WalkingHorizontal = m_Player.FindAction("WalkingHorizontal", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_DropItem_01 = m_Inventory.FindAction("DropItem_01", throwIfNotFound: true);
        m_Inventory_DropItem_02 = m_Inventory.FindAction("DropItem_02", throwIfNotFound: true);
        m_Inventory_DropItem_03 = m_Inventory.FindAction("DropItem_03", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Melee;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_WalkingVertical;
    private readonly InputAction m_Player_WalkingHorizontal;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Melee => m_Wrapper.m_Player_Melee;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @WalkingVertical => m_Wrapper.m_Player_WalkingVertical;
        public InputAction @WalkingHorizontal => m_Wrapper.m_Player_WalkingHorizontal;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Melee.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @WalkingVertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingVertical;
                @WalkingVertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingVertical;
                @WalkingVertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingVertical;
                @WalkingHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingHorizontal;
                @WalkingHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingHorizontal;
                @WalkingHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalkingHorizontal;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @WalkingVertical.started += instance.OnWalkingVertical;
                @WalkingVertical.performed += instance.OnWalkingVertical;
                @WalkingVertical.canceled += instance.OnWalkingVertical;
                @WalkingHorizontal.started += instance.OnWalkingHorizontal;
                @WalkingHorizontal.performed += instance.OnWalkingHorizontal;
                @WalkingHorizontal.canceled += instance.OnWalkingHorizontal;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_DropItem_01;
    private readonly InputAction m_Inventory_DropItem_02;
    private readonly InputAction m_Inventory_DropItem_03;
    public struct InventoryActions
    {
        private @Controls m_Wrapper;
        public InventoryActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DropItem_01 => m_Wrapper.m_Inventory_DropItem_01;
        public InputAction @DropItem_02 => m_Wrapper.m_Inventory_DropItem_02;
        public InputAction @DropItem_03 => m_Wrapper.m_Inventory_DropItem_03;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @DropItem_01.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_01;
                @DropItem_01.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_01;
                @DropItem_01.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_01;
                @DropItem_02.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_02;
                @DropItem_02.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_02;
                @DropItem_02.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_02;
                @DropItem_03.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_03;
                @DropItem_03.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_03;
                @DropItem_03.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem_03;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DropItem_01.started += instance.OnDropItem_01;
                @DropItem_01.performed += instance.OnDropItem_01;
                @DropItem_01.canceled += instance.OnDropItem_01;
                @DropItem_02.started += instance.OnDropItem_02;
                @DropItem_02.performed += instance.OnDropItem_02;
                @DropItem_02.canceled += instance.OnDropItem_02;
                @DropItem_03.started += instance.OnDropItem_03;
                @DropItem_03.performed += instance.OnDropItem_03;
                @DropItem_03.canceled += instance.OnDropItem_03;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    private int m_KeyboadandMouseSchemeIndex = -1;
    public InputControlScheme KeyboadandMouseScheme
    {
        get
        {
            if (m_KeyboadandMouseSchemeIndex == -1) m_KeyboadandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboad and Mouse");
            return asset.controlSchemes[m_KeyboadandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnWalkingVertical(InputAction.CallbackContext context);
        void OnWalkingHorizontal(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnDropItem_01(InputAction.CallbackContext context);
        void OnDropItem_02(InputAction.CallbackContext context);
        void OnDropItem_03(InputAction.CallbackContext context);
    }
}
