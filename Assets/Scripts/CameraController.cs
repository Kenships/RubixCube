using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    
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

    private void OnMouseMove(Vector2 movementDelta)
    {
        if (!_isMouseDown) return;
        
        Vector3 axis = new Vector3(-movementDelta.y, movementDelta.x, 0).normalized;
        
        transform.Rotate(axis, movementDelta.magnitude);
    }
}
