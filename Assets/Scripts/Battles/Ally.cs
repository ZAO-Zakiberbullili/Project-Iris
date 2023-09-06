using UnityEngine;

public class Ally : MonoBehaviour 
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _distanceFromPlayer = 5f;
    
    private GameObject player;

    void Start() 
    {
        InitPrologue.OnPlayerInitiated += HandlePlayerInitiated;
    }

    private void HandlePlayerInitiated()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // if camera doesn't see ally -> teleport to player

        // move only on tiles

        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > _distanceFromPlayer)
        {
            transform.position += Time.deltaTime * _moveSpeed * directionToPlayer;
        }
        // else player is nearby, do nothing
    }
}