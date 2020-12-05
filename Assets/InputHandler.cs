// GENERATED AUTOMATICALLY FROM 'Assets/InputHandler.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputHandler : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputHandler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputHandler"",
    ""maps"": [
        {
            ""name"": ""DropItem"",
            ""id"": ""795209ed-a4e5-4dbb-8f3e-2c4d0a6e8f7c"",
            ""actions"": [
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""3cb1e013-e899-4104-bb84-67b38ebe6214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem1"",
                    ""type"": ""Button"",
                    ""id"": ""3da28f4d-fbd6-4c25-b617-fe7d23a330d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem2"",
                    ""type"": ""Button"",
                    ""id"": ""ee49b9c7-1fdc-4470-a935-6895ecd116fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""beba73cb-fde2-46f1-b464-879b2c8fd32a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd4c31c1-bfcf-4b26-bc4f-4e54e5929915"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1cc30b0-f846-40f0-a140-43c5eee43ab6"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DropItem
        m_DropItem = asset.FindActionMap("DropItem", throwIfNotFound: true);
        m_DropItem_DropItem = m_DropItem.FindAction("DropItem", throwIfNotFound: true);
        m_DropItem_DropItem1 = m_DropItem.FindAction("DropItem1", throwIfNotFound: true);
        m_DropItem_DropItem2 = m_DropItem.FindAction("DropItem2", throwIfNotFound: true);
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

    // DropItem
    private readonly InputActionMap m_DropItem;
    private IDropItemActions m_DropItemActionsCallbackInterface;
    private readonly InputAction m_DropItem_DropItem;
    private readonly InputAction m_DropItem_DropItem1;
    private readonly InputAction m_DropItem_DropItem2;
    public struct DropItemActions
    {
        private @InputHandler m_Wrapper;
        public DropItemActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @DropItem => m_Wrapper.m_DropItem_DropItem;
        public InputAction @DropItem1 => m_Wrapper.m_DropItem_DropItem1;
        public InputAction @DropItem2 => m_Wrapper.m_DropItem_DropItem2;
        public InputActionMap Get() { return m_Wrapper.m_DropItem; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DropItemActions set) { return set.Get(); }
        public void SetCallbacks(IDropItemActions instance)
        {
            if (m_Wrapper.m_DropItemActionsCallbackInterface != null)
            {
                @DropItem.started -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem;
                @DropItem1.started -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem1;
                @DropItem1.performed -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem1;
                @DropItem1.canceled -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem1;
                @DropItem2.started -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem2;
                @DropItem2.performed -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem2;
                @DropItem2.canceled -= m_Wrapper.m_DropItemActionsCallbackInterface.OnDropItem2;
            }
            m_Wrapper.m_DropItemActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @DropItem1.started += instance.OnDropItem1;
                @DropItem1.performed += instance.OnDropItem1;
                @DropItem1.canceled += instance.OnDropItem1;
                @DropItem2.started += instance.OnDropItem2;
                @DropItem2.performed += instance.OnDropItem2;
                @DropItem2.canceled += instance.OnDropItem2;
            }
        }
    }
    public DropItemActions @DropItem => new DropItemActions(this);
    public interface IDropItemActions
    {
        void OnDropItem(InputAction.CallbackContext context);
        void OnDropItem1(InputAction.CallbackContext context);
        void OnDropItem2(InputAction.CallbackContext context);
    }
}
