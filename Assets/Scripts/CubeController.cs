using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Transform pivot;
    private Collider[] colliders = new Collider[9];
    private bool _isRotating = false;
    private Stack <(Vector3 norm, float angle)> _history = new();
    public float Rotation { get; set; } = 90f;

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
        _history.Push((hit.normal, Rotation));
        Rotate(hit.normal, Rotation);
    }

    private void Rotate(Vector3 normal, float angle)
    {
        _isRotating = true;
        Quaternion boxRotation = Quaternion.FromToRotation(Vector3.up, normal);
        Physics.OverlapBoxNonAlloc(normal, new Vector3(3f, 0.4f, 3f), colliders, boxRotation);
        foreach (var col in colliders)
        {
            col.transform.SetParent(pivot);
        }
    
        Quaternion targetRotation = Quaternion.AngleAxis(angle, normal)*pivot.rotation;
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

    private IEnumerator SolveRoutine()
    {
        while (_history.Count > 0)
        {
            var unit = _history.Pop();
            while (_isRotating)
            {
                yield return null;
            }
            Rotate(unit.norm, -unit.angle);
        }
    }
    public void Solve()
    {
        StartCoroutine(SolveRoutine());
    }
}
