using UnityEngine;

namespace Pathfinding
{
  class AStar
  {
    public Node[] FindPath(Node startNode, Node targetNode, Node[,] grid)
    {
      List<Node> openNodes = new List<Node>();
      List<Node> closedNodes = new List<Node>();
      openNodes.Add(startNode);
      while (openNodes.Count > 0)
      {
        Node currentNode = openNodes[0];
        for (int i = 0; i < openNodes.Count; i++)
        {
          if (openNodes[i].f < currentNode.f)
          {
            currentNode = openNodes[i];
          }
        }
        if (currentNode == targetNode)
        {
          // found path
          return GetPath(currentNode).ToArray();
        }
        closedNodes.Add(currentNode);
        openNodes.Remove(currentNode);

        foreach (Node neighbour in currentNode.GetNeighbours(grid))
        {
          if (!closedNodes.Contains(neighbour) && neighbour.walkable)
          {
            int tempG = currentNode.g + 1;
            if (openNodes.Contains(neighbour))
            {
              if (tempG < neighbour.g)
              {
                neighbour.g = tempG;
                neighbour.previous = currentNode;
              }
            }
            else
            {
              neighbour.g = tempG;
              neighbour.previous = currentNode;
              openNodes.Add(neighbour);
            }
            neighbour.h = CalcHeuristic(neighbour, targetNode);
            neighbour.f = neighbour.g + neighbour.h;

          }

        }
      }
      // unable to find a path
      return null;
    }

    List<Node> GetPath(Node current)
    {
      List<Node> path = new List<Node>();
      Node temp = current;
      while (temp.previous != null)
      {
        path.Add(temp);
        temp = temp.previous;
      }
      return path;
    }

    int CalcHeuristic(Node startNode, Node targetNode)
    {
      int x = Mathf.Abs(startNode.gridPosition.x - targetNode.gridPosition.x);
      int y = Mathf.Abs(startNode.gridPosition.y - targetNode.gridPosition.y);
      return x + y;
    }
  }

  class Node
  {
    public Vector2Int gridPosition;
    public int f = 0;
    public int g = 0;
    public int h = 0;
    public Node previous = null;
    public bool walkable = true;
    public Node(Vector2Int _gridPosition)
    {
      gridPosition = _gridPosition;
    }

    public List<Node> GetNeighbours(Node[,] grid)
    {
      List<Node> neighbours = new List<Node>();
      Vector2Int[] vector2Ints = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
      foreach (Vector2Int vector2Int in vector2Ints)
      {
        Vector2Int possibleNeighbourPosition = vector2Int + gridPosition;
        if (possibleNeighbourPosition.x >= 0 &&
            possibleNeighbourPosition.y >= 0 &&
            possibleNeighbourPosition.x <= grid.GetUpperBound(0) &&
            possibleNeighbourPosition.y <= grid.GetUpperBound(1))
        {
          neighbours.Add(grid[possibleNeighbourPosition.x, possibleNeighbourPosition.y]);
        }
      }
      return neighbours;
    }
  }
}
