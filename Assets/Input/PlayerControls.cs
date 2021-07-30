// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Vehicle Controls"",
            ""id"": ""c4b221e8-ca63-4984-8f3f-ad235bef6481"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""059e3eb0-61a2-42d8-b658-5dbeb6a7cea7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dcc5b880-439e-4a63-bf2d-081e22ada5e0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""PassThrough"",
                    ""id"": ""880ccb95-4cc9-4f8b-b8b0-3b6e39f1c412"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VerticalRotation"",
                    ""type"": ""Button"",
                    ""id"": ""ac976749-db38-4a97-a74d-60813970cb1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HorizontalChange"",
                    ""type"": ""Value"",
                    ""id"": ""0764b6c3-550b-4633-8d13-c7df607371be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""154f7c83-9ed5-4bbe-8130-c513312d3454"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d35fb240-295e-400c-802e-5cc6adfce124"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c784f6ad-aa80-425e-b757-5116f3d43b58"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3c222d45-b174-4fea-9383-167737b0e290"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3427b05a-59c6-4718-b3fd-0c538414c5c4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b4d7599b-79ec-437f-b8b8-608454e4beba"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f8b3742e-e10c-4c2f-848c-1ea3a78d4492"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ccaeb05f-d66b-471b-838a-10ff16c3629c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9f6d99bf-4cc0-4aff-97bc-d083d9b94af2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""60cc76ae-64bf-4419-b19f-a8bb85582c4c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""450f347d-5ac0-4958-8744-8d089dd88663"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""28ec90cb-ec09-46fa-b1fe-fd59eb7d685e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6d7391e6-2e90-4ada-bcff-d2b590e8c650"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""VerticalRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""aa99db06-adcb-4f9e-a5a3-30f9c68cd66a"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""VerticalRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0e0fad74-b481-4a46-af12-635af11f2570"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""HorizontalChange"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""426e40da-ba0c-49ac-ade8-e94f5c351b9b"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""HorizontalChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9115bfc3-a9aa-4313-b3b0-8ca2f5fd11a9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & Mouse"",
                    ""action"": ""HorizontalChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard & Mouse"",
            ""bindingGroup"": ""KeyBoard & Mouse"",
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
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Vehicle Controls
        m_VehicleControls = asset.FindActionMap("Vehicle Controls", throwIfNotFound: true);
        m_VehicleControls_Fire = m_VehicleControls.FindAction("Fire", throwIfNotFound: true);
        m_VehicleControls_Move = m_VehicleControls.FindAction("Move", throwIfNotFound: true);
        m_VehicleControls_Turn = m_VehicleControls.FindAction("Turn", throwIfNotFound: true);
        m_VehicleControls_VerticalRotation = m_VehicleControls.FindAction("VerticalRotation", throwIfNotFound: true);
        m_VehicleControls_HorizontalChange = m_VehicleControls.FindAction("HorizontalChange", throwIfNotFound: true);
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

    // Vehicle Controls
    private readonly InputActionMap m_VehicleControls;
    private IVehicleControlsActions m_VehicleControlsActionsCallbackInterface;
    private readonly InputAction m_VehicleControls_Fire;
    private readonly InputAction m_VehicleControls_Move;
    private readonly InputAction m_VehicleControls_Turn;
    private readonly InputAction m_VehicleControls_VerticalRotation;
    private readonly InputAction m_VehicleControls_HorizontalChange;
    public struct VehicleControlsActions
    {
        private @PlayerControls m_Wrapper;
        public VehicleControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_VehicleControls_Fire;
        public InputAction @Move => m_Wrapper.m_VehicleControls_Move;
        public InputAction @Turn => m_Wrapper.m_VehicleControls_Turn;
        public InputAction @VerticalRotation => m_Wrapper.m_VehicleControls_VerticalRotation;
        public InputAction @HorizontalChange => m_Wrapper.m_VehicleControls_HorizontalChange;
        public InputActionMap Get() { return m_Wrapper.m_VehicleControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VehicleControlsActions set) { return set.Get(); }
        public void SetCallbacks(IVehicleControlsActions instance)
        {
            if (m_Wrapper.m_VehicleControlsActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnFire;
                @Move.started -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnMove;
                @Turn.started -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnTurn;
                @VerticalRotation.started -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnVerticalRotation;
                @VerticalRotation.performed -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnVerticalRotation;
                @VerticalRotation.canceled -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnVerticalRotation;
                @HorizontalChange.started -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnHorizontalChange;
                @HorizontalChange.performed -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnHorizontalChange;
                @HorizontalChange.canceled -= m_Wrapper.m_VehicleControlsActionsCallbackInterface.OnHorizontalChange;
            }
            m_Wrapper.m_VehicleControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @VerticalRotation.started += instance.OnVerticalRotation;
                @VerticalRotation.performed += instance.OnVerticalRotation;
                @VerticalRotation.canceled += instance.OnVerticalRotation;
                @HorizontalChange.started += instance.OnHorizontalChange;
                @HorizontalChange.performed += instance.OnHorizontalChange;
                @HorizontalChange.canceled += instance.OnHorizontalChange;
            }
        }
    }
    public VehicleControlsActions @VehicleControls => new VehicleControlsActions(this);
    private int m_KeyBoardMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardMouseScheme
    {
        get
        {
            if (m_KeyBoardMouseSchemeIndex == -1) m_KeyBoardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoard & Mouse");
            return asset.controlSchemes[m_KeyBoardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IVehicleControlsActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnVerticalRotation(InputAction.CallbackContext context);
        void OnHorizontalChange(InputAction.CallbackContext context);
    }
}
