using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private Vector2 _currentMove;
    private Vector2 _currentMoveBeforeIdle;

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMove = context.ReadValue<Vector2>();
    }

    void Update()
    {
        _anim.SetFloat("X", _currentMove.x);
        _anim.SetFloat("Y", _currentMove.y);

        if (_currentMove.x != 0 || _currentMove.y != 0)
        {
            _anim.SetBool("Idle", false);
            _currentMoveBeforeIdle = _currentMove;
        }
        else 
        {
            _anim.SetBool("Idle", true);

            if (_currentMoveBeforeIdle.x > 0)
            {
                _anim.SetFloat("X", 1f);
                _anim.SetFloat("Y", 0f);
            }
            else if (_currentMoveBeforeIdle.x < 0)
            {
                _anim.SetFloat("X", -1f);
                _anim.SetFloat("Y", 0f);
            }
            else
            {
                _anim.SetFloat("X", 0f);
                _anim.SetFloat("Y", 1f);
            }
        }
    }
}
