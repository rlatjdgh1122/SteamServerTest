using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Input/Reader", fileName = "New Input reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> RunEvent;

    private Controls _controlAction;

    private void OnEnable()
    {
        if (_controlAction == null)
        {
            _controlAction = new Controls();
            _controlAction.Player.SetCallbacks(this);
        }
        _controlAction.Player.Enable(); 
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }
    public void OnFire(InputAction.CallbackContext context)
    {

    }

    public void OnRun(InputAction.CallbackContext context)
    {
        RunEvent?.Invoke(context.performed);
    }
}
