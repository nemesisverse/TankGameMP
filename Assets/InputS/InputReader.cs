using UnityEngine;
using System;
using static Controlls;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{

    public event Action<bool> PrimaryFireEvent;
    public event Action<Vector2> MoveEvent;
    private Controlls controlls;

    private void OnEnable() //This script here is activating InputSystem
    {
        if(controlls == null)
        {
            controlls = new Controlls();
            controlls.Player.SetCallbacks(this);

        }

        controlls.Player.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)//Trigarring an event
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        if (context.performed) {
            PrimaryFireEvent?.Invoke(true);
        }
        else if(context.canceled){
            PrimaryFireEvent?.Invoke(false);
        }
    }
}
