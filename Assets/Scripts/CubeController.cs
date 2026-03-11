using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Transform pivot;
    [SerializeField] private float turnDuration = 0.3f;
    
    private Stack<(Vector3, float)> _moveHistory = new();
    private Collider[] _buffer = new Collider[9]; 
    private bool _isRotating;

    private float _rotationAngle = 90f;
    
    private void Start()
    {
        inputReader.OnLeftClick += OnLeftClick;
    }

    private void OnDestroy()
    {
        inputReader.OnLeftClick -= OnLeftClick;
    }

    private void OnLeftClick()
    {
        if (_isRotating) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        
        _moveHistory.Push((hit.normal, _rotationAngle));
        
        Rotate(hit.normal, _rotationAngle);
    }

    private void Rotate(Vector3 normal, float rotation = 90f)
    {
        Quaternion normalRotation = Quaternion.FromToRotation(Vector3.up, normal);
        
        if (Physics.OverlapBoxNonAlloc(normal, new Vector3(3f, 0.4f, 3f), _buffer, normalRotation) == 0) return;
        
        ParentAllCollidersTo(pivot);
        
        Quaternion targetRotation = Quaternion.AngleAxis(rotation, normal) * pivot.rotation;

        _isRotating = true;
        
        Tween.LocalRotation(
            target: pivot,
            endValue: targetRotation,
            duration: turnDuration
        ).OnComplete(() =>
            {
                ParentAllCollidersTo(transform);
                _isRotating = false;
            }
        );
    }
    
    #region Solving Cube

    public void Solve()
    {
        StartCoroutine(SolveRoutine());
    }

    private IEnumerator SolveRoutine()
    {
        while (_moveHistory.TryPop(out (Vector3 normal, float rotation) move))
        {
            while (_isRotating)
            {
                yield return null;
            }
            
            Rotate(move.normal, -move.rotation);
        }
    }
    
    #endregion

    #region Utility
    
    private void ParentAllCollidersTo(Transform parent)
    {
        foreach (Collider col in _buffer)
        {
            col.transform.SetParent(parent);
        }
    }
    
    public void SetRotation(float rotation)
    {
        _rotationAngle = rotation;
    }
    
    #endregion
}
