using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Transform pivot;
    private Collider[] colliders = new Collider[9];
    private bool _isRotating = false;
    
    private void Start()
    {
        inputReader.OnLeftClick += OnLeftClick;
    }

    private void OnLeftClick()
    {
        if (_isRotating) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        } 
        _isRotating = true;
        Quaternion boxRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Physics.OverlapBoxNonAlloc(hit.normal, new Vector3(3f, 0.4f, 3f), colliders, boxRotation);
        foreach (var col in colliders)
        {
            col.transform.SetParent(pivot);
        }
    
        Quaternion targetRotation = Quaternion.AngleAxis(90, hit.normal)*pivot.rotation;
        Tween.Rotation(
            target: pivot,
            endValue: targetRotation,
            duration: 0.2f
        ).OnComplete(() =>
                     {
                         foreach (var col in colliders)
                         {
                             col.transform.SetParent(transform);
                         }
                         _isRotating = false;
                     });
    }
}
