# unity3d-a-star-pathfinding
An A* pathfinding class built for Unity3d in c#

## purpose
An implementation of A* pathfinding in c# for Unity3D

## how to use
```c#
using Pathfinding;

Astar astar = new Astar();

// create grid of Node's
Node[,] grid = [gridLengthX, gridLengthY];
// loop over grid and initialise nodes
Node[] path = astar.FindPath(grid[startPositionX, startPositionY, targetPositionX, targetPositionY, grid]);

if (path != null) {
  // path was found
}

```

