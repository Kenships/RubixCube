using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Transform pivot;
    private Collider[] colliders = new Collider[9];

    private void Start()
    {
        inputReader.OnLeftClick += OnLeftClick;
    }

    private void OnLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }
        Quaternion boxRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Physics.OverlapBoxNonAlloc(hit.normal, new Vector3(3f, 0.4f, 3f), colliders, boxRotation);
        foreach (var col in colliders)
        {
            col.transform.SetParent(pivot);
        } 
        pivot.Rotate(hit.normal, 90, Space.World);
        foreach (var col in colliders)
        {
            col.transform.SetParent(transform);
        }
    }
}
