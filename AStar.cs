using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding
{
  class AStar
  {
    public int Heuristic(Node a, Node b) {
      int x = Mathf.Abs(a.mapPosition.x - b.mapPosition.x);
      int y = Mathf.Abs(a.mapPosition.y - b.mapPosition.y);
      return x + y;
    }

    public Node[] GetPath(Node endNode) {
        List<Node> path = new List<Node>();
        Node temp = endNode;
        path.Add(temp);
        while (temp.previous != null) {
            path.Add(temp.previous);
            temp = temp.previous;
        }
        return path.ToArray();
    }

    public Node FindPath(Node[,] map, Node start, Node target) {
      List<Node> openNodes = new List<Node>();
      List<Node> closedNodes = new List<Node>();
      start.previous = null;
      openNodes.Add(start);
      while (openNodes.Count > 0) {
        Node currentNode = openNodes[0];
        for (int i = 0; i < openNodes.Count; i++) {
          if (openNodes[i].f < currentNode.f) {
            currentNode = openNodes[i];
          }

          if (openNodes[i].f == currentNode.f) {
            if (openNodes[i].g > currentNode.g) {
                currentNode = openNodes[i];
            }
          }
        }

        if (currentNode == target) {
          return currentNode;
        }

        openNodes.Remove(currentNode);
        closedNodes.Add(currentNode);
        foreach(Node neighbour in currentNode.GetNeighbours(map)) {
          if (!closedNodes.Contains(neighbour)) {
            int tempG = currentNode.g + Heuristic(neighbour, currentNode);
            if (!openNodes.Contains(neighbour)) {
                openNodes.Add(neighbour);
            } else if (tempG >= neighbour.g) {
              continue;
            }

            neighbour.g = tempG;
            neighbour.h = Heuristic(neighbour, target);
            neighbour.f = neighbour.g + neighbour.h;
            neighbour.previous = currentNode;
          }
        }


      }
      return null;
    }

  }

  class Node {
    public int h = 0;
    public int g = 0;
    public int f = 0;
    public int vh = 0;
    public bool walkable = true;
    public Node previous = null;
    public Vector2Int mapPosition;
    public Node(Vector2Int mapPosition) {
      this.mapPosition = mapPosition;
    }

    public Node[] GetNeighbours(Node[,] map) {
        List<Node> neighbours = new List<Node>();
        Vector2Int[] vector2Ints = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        foreach (Vector2Int vector2Int in vector2Ints)
        {
            Vector2Int possibleNeighbourPosition = vector2Int + mapPosition;
            if (possibleNeighbourPosition.x >= 0 &&
                possibleNeighbourPosition.y >= 0 &&
                possibleNeighbourPosition.x <= map.GetUpperBound(0) &&
                possibleNeighbourPosition.y <= map.GetUpperBound(1))
            {
                Node neighbour = map[possibleNeighbourPosition.x, possibleNeighbourPosition.y];
                if (neighbour.walkable) {
                    neighbours.Add(neighbour);
                }
            }
        }

        return neighbours.ToArray();
    }
  }
}
