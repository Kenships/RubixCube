using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private float sensitivity=1;
    private bool _isRightClicking;

    private void Start()
    {
        inputReader.OnRightClick+= OnRightClick;
        inputReader.OnRightRelease+= OnRightRelease;
        inputReader.OnMouseMove+= OnMouseMove;
    }

    private void OnDestroy()
    {
        inputReader.OnRightClick-= OnRightClick;
        inputReader.OnRightRelease-= OnRightRelease;
        inputReader.OnMouseMove-= OnMouseMove;
    }
    
    private void OnMouseMove(Vector2 distance)
    {
        if (!_isRightClicking)
        {
            return;
        }

        Vector3 distance3 = new Vector3(-1 * distance.y, distance.x, 0);
        transform.Rotate(distance3, distance.magnitude * sensitivity);
    }

    private void OnRightRelease()
    {
        _isRightClicking=false;
    }

    private void OnRightClick()
    {
        _isRightClicking=true;
    }
}
