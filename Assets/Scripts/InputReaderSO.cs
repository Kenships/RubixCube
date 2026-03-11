using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    public UnityAction<Vector2> OnMouseMove;
    public UnityAction OnLeftClick;
    public UnityAction OnLeftRelease;
    public UnityAction OnRightClick;
    public UnityAction OnRightRelease;
    
    private InputSystem_Actions inputActions;
    
    private void OnEnable()
    {
        inputActions ??= new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.SetCallbacks(null);
        inputActions.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var delta = context.ReadValue<Vector2>();
            OnMouseMove?.Invoke(delta);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnLeftClick?.Invoke();
        }

        if (context.canceled)
        {
            OnLeftRelease?.Invoke();
        }
    }

    public void OnAltAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRightClick?.Invoke();
        }

        if (context.canceled)
        {
            OnRightRelease?.Invoke();
        }
    }
}
