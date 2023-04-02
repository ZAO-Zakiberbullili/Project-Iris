using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Grid _grid;
    public Transform _player;
    [SerializeField] private float _moveSpeed = 5f;

    private List<Node> path;
    private int currentNodeIndex;
    private Vector3 targetPosition;
    
    void Update() 
    {
        if (path == null || currentNodeIndex >= path.Count) 
        {
            path = FindPath(transform.position, _player.position);
            currentNodeIndex = 0;
        }

        if (path != null && currentNodeIndex < path.Count) 
        {
            Node currentNode = path[currentNodeIndex];

            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * _moveSpeed;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) 
            {
                currentNodeIndex++;

                if (currentNodeIndex < path.Count) 
                {
                    targetPosition = path[currentNodeIndex].worldPosition;
                }
            }
        }   

        if (Vector3.Distance(transform.position, _player.position) < 2f && path != null && currentNodeIndex < path.Count) 
        {
            currentNodeIndex = 0;
            targetPosition = _player.position;
        }
    }  

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos) 
    {
        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) 
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i ++) 
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)) 
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbor in _grid.GetNeighbors(currentNode)) 
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor)) 
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor)) 
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor)) 
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode) 
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) 
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    int GetDistance(Node nodeA, Node nodeB) 
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY) 
        {
            return 14 * dstY + 10 * (dstX - dstY);
        } 
        else 
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
