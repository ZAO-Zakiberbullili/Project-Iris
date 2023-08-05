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

    void Start()
    {
        sidebrick = false;
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

        /*
        Vector3Int cellPosition = _tilemap.WorldToCell(_feet.position);
        Vector3Int playerMove = new Vector3Int((int)_currentMove.x, (int)_currentMove.y, 0);
        Vector3Int targetTileCoord = cellPosition + playerMove;
        TileBase targetTile = _tilemap.GetTile(targetTileCoord);
        if (targetTile)
        {
            transform.position += Time.deltaTime * _moveSpeed * new Vector3(_currentMove.x, _currentMove.y, 0f);
        }

        Vector3Int targetTileBelowCoord = new Vector3Int(targetTileCoord.x, targetTileCoord.y, targetTileCoord.z - 1);
        TileBase targetTileBelow = _tilemap.GetTile(targetTileBelowCoord);
        if (_tilemap.GetSprite(cellPosition)?.name == "Single_sidebrick_tile" && targetTileBelow)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetTileBelowCoord.z);
        }
           
        Vector3Int targetTileAboveCoord = new Vector3Int(targetTileCoord.x, targetTileCoord.y, targetTileCoord.z + 1);
        TileBase targetTileAbove = _tilemap.GetTile(targetTileAboveCoord);
        if (_tilemap.GetSprite(cellPosition + new Vector3Int(0, 0, 1))?.name == "Single_sidebrick_tile" && targetTileAbove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetTileAboveCoord.z);
        }*/
    }
}
