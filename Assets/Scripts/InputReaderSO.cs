using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    public UnityAction<Vector2> OnMouseMove;
    public UnityAction OnClick;
    public UnityAction OnRelease;
    
    private InputSystem_Actions inputActions;
    
    private void OnEnable()
    {
        inputActions ??= new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
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
            OnClick?.Invoke();
        }

        if (context.canceled)
        {
            OnRelease?.Invoke();
        }
    }
}
