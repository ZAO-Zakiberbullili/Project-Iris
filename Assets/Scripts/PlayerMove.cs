using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Transform _feet;
    private Vector2 _currentMove;
    private float _jump;

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMove = context.ReadValue<Vector2>().normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jump = context.ReadValue<float>();
    }

    private bool sidebrick;

    void Awake()
    {
        sidebrick = false;
        _tilemap = GameObject.FindGameObjectWithTag("Grid").GetComponent<Tilemap>();
    }
    
    void Update()
    {
        Vector3Int cellPosition = _tilemap.WorldToCell(_feet.position);
        Vector3Int playerMove = new Vector3Int((int)_currentMove.x, (int)_currentMove.y, 0);
        Vector3Int targetTileCoord = cellPosition + playerMove;
        TileBase targetTile = _tilemap.GetTile(targetTileCoord);

        if (_tilemap.GetSprite(cellPosition)?.name == "Single_sidebrick_tile" || _tilemap.GetSprite(cellPosition + new Vector3Int(0, 0, 1))?.name == "Single_sidebrick_tile")
        {
            sidebrick = true;
        }

        if (targetTile || sidebrick)
        {
            transform.position += Time.deltaTime * _moveSpeed * new Vector3(_currentMove.x, _currentMove.y, 0f);
        }

        if (sidebrick && (_tilemap.GetSprite(cellPosition)?.name != "Single_sidebrick_tile" || _tilemap.GetSprite(cellPosition + new Vector3Int(0, 0, 1))?.name != "Single_sidebrick_tile"))
        {
            sidebrick = false;
            if (_tilemap.GetTile(_tilemap.WorldToCell(_feet.position + new Vector3Int(0, 0, -1))))
            {
                transform.position += new Vector3(0f, 0f, -1f);
            }
            else if (_tilemap.GetTile(_tilemap.WorldToCell(cellPosition)))
            {
                goto SKIP;
            }
            else if (_tilemap.GetTile(_tilemap.WorldToCell(_feet.position + new Vector3Int(0, 0, 1))))
            {
                transform.position += new Vector3(0f, 0f, 1f);
            }
        }

        SKIP:   ;
    }

    public void FindTileLayer()
    {
        if (_tilemap.GetTile(_tilemap.WorldToCell(_feet.position + new Vector3Int(0, 0, -1))))
            {
                transform.position += new Vector3(0f, 0f, -1f);
            }
        else if (_tilemap.GetTile(_tilemap.WorldToCell(_feet.position + new Vector3Int(0, 0, 1))))
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
    }

    public void SavePlayerPosition()
    {
        SaveManager.Instance.GetCurrentSaveData().player.x = transform.position.x;
        SaveManager.Instance.GetCurrentSaveData().player.y = transform.position.y;

        if (transform.position.z == -1f || transform.position.z == 0f)
        {
            SaveManager.Instance.GetCurrentSaveData().player.z = transform.position.z;
        }
        else
        {
            SaveManager.Instance.GetCurrentSaveData().player.z = 0f;
        }
        
    }
}
