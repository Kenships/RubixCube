using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private Transform camera;
    
        private bool _isMouseDown;
    
        private void Start()
        {
            inputReader.OnMouseMove += OnMouseMove;
            inputReader.OnRightClick += OnClick;
            inputReader.OnRightRelease += OnRelease;
        }
    
        private void OnDestroy()
        {
            inputReader.OnMouseMove -= OnMouseMove;
            inputReader.OnRightClick -= OnClick;
            inputReader.OnRightRelease -= OnRelease;
        }

        private void OnRelease()
        {
            _isMouseDown = false;
        }

        private void OnClick()
        {
            _isMouseDown = true;
        }

        private Vector3 axis;

        private void OnMouseMove(Vector2 direction)
        {
            if (!_isMouseDown) return;
        
            axis = new Vector3(-direction.y, direction.x, 0).normalized;
        
            transform.Rotate(axis, direction.magnitude, Space.Self);
        }

        private void OnDrawGizmos()
        {
            if (transform)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + axis * 10f);
            }
        }
    }
}
