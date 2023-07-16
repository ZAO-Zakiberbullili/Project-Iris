using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _currentMove;

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMove = context.ReadValue<Vector2>();
    }
    
    void Update()
    {
        transform.position += Time.deltaTime * _moveSpeed * new Vector3(_currentMove.x, _currentMove.y, 0f);
        
        // for testing purposes: saving should be less often
        SaveManager.Instance.GetCurrentSaveData().player.x = transform.position.x;
        SaveManager.Instance.GetCurrentSaveData().player.y = transform.position.y;
        SaveManager.Instance.GetCurrentSaveData().saveTime = DateTime.Now;
    }
}
