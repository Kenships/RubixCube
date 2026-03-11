using UnityEngine;

public class CubeOrientation : MonoBehaviour
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

    private void OnMouseMove(Vector2 direction)
    {
        if (!_isMouseDown) return;
        
        var axis = new Vector3(direction.y, -direction.x, 0).normalized;
        
        transform.Rotate(axis, direction.magnitude, Space.World);
    }
}
