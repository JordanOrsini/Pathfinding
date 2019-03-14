# Pathfinding

To run the game please open the scene Main.unity located in the Assets/Scenes
folder in the project directory.

In order to view the grid, computed shortest path, fill, clusters and/or connections
to POV nodes you must be in scene view and not in the game view.


Basic inputs (must be in game view):

Alpha 1: Grid + Dijkstra

Alpha 2: Grid + A*

Alpha 3: Grid + A* + Cluster

Alpha 4: POV + Dijkstra

Alpha 5: POV + A*

Alpha 6: POV + A* + Cluster


Basic Legend:

White: Walkable node

Red: Un-walkable node

Blue: Node that was once on the open list (visited)

Green: Computed shortest path


Cluster legend:

Yellow: Cluster 1

Pink: Cluster 2

Orange: Cluster 3

Cyan: Cluster 4

Purple: Cluster 5
