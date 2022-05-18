// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

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
            ""name"": ""clicks"",
            ""id"": ""9bc7167a-d1e0-42f9-b3b3-ebeb43950dec"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""aa74035e-ec29-461a-bb98-77cfc930784d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b7b0c99e-6636-4e05-b80d-c7f0aea30274"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""380845e6-5002-4121-8ade-373900efc135"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6427f61-2fa2-432c-a110-2e1e2740ffd3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // clicks
        m_clicks = asset.FindActionMap("clicks", throwIfNotFound: true);
        m_clicks_Click = m_clicks.FindAction("Click", throwIfNotFound: true);
        m_clicks_Position = m_clicks.FindAction("Position", throwIfNotFound: true);
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

    // clicks
    private readonly InputActionMap m_clicks;
    private IClicksActions m_ClicksActionsCallbackInterface;
    private readonly InputAction m_clicks_Click;
    private readonly InputAction m_clicks_Position;
    public struct ClicksActions
    {
        private @Controls m_Wrapper;
        public ClicksActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_clicks_Click;
        public InputAction @Position => m_Wrapper.m_clicks_Position;
        public InputActionMap Get() { return m_Wrapper.m_clicks; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ClicksActions set) { return set.Get(); }
        public void SetCallbacks(IClicksActions instance)
        {
            if (m_Wrapper.m_ClicksActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_ClicksActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_ClicksActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_ClicksActionsCallbackInterface.OnClick;
                @Position.started -= m_Wrapper.m_ClicksActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_ClicksActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_ClicksActionsCallbackInterface.OnPosition;
            }
            m_Wrapper.m_ClicksActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
            }
        }
    }
    public ClicksActions @clicks => new ClicksActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IClicksActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
    }
}
