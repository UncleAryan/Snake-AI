# Snake AI

A Snake game simulation built in Unity where the snake navigates autonomously using the A* pathfinding algorithm. The snake continuously searches for food on a configurable grid. When no path to food exists, it moves to an adjacent open node to free up space and retries.

---

## Requirements

- Unity 6000.4.10f1 (Unity 6)
- Universal Render Pipeline 

---

## Setup

1. Clone or download the repository.
2. Open Unity Hub and click **Open > Add project from disk**.
3. Select the root folder of this project.
4. Unity will import assets and resolve packages automatically. Wait for the import to finish before entering Play mode.

---

## Running the Project

### Via the Scene

1. In the **Project** window, navigate to `Assets/Scenes/`.
2. Double-click `SampleScene.unity` to open it.
3. Press the **Play** button in the toolbar. The simulation starts immediately.

### Via the Hierarchy

The scene contains a **GameController** GameObject that drives the entire simulation. You can also locate it directly:

1. Open `SampleScene.unity`.
2. In the **Hierarchy** window, select the `GameController` object.
3. In the **Inspector**, you will find the following configurable fields before pressing Play:

| Field | Description |
|---|---|
| `Time Step` | Delay in seconds between each snake move (default: 0.1) |
| `Map Data > Row` | Number of rows in the grid |
| `Map Data > Col` | Number of columns in the grid |
| `Heuristics > Manhattan Heuristics` | Use Manhattan distance for A* |
| `Heuristics > Euclidean Heuristics` | Use Euclidean distance for A* |

The camera adjusts automatically to fit the grid size. No manual camera setup is needed.

---

## Project Structure

```
Assets/
  Scenes/
    SampleScene.unity         # Open this to run the project
  Scripts/
    GameController.cs         # Initializes all systems on Start
    MapData.cs                # Generates the grid as a 2D array
    Graph.cs                  # Holds all nodes and neighbor relationships
    Node.cs                   # Stores A* cost values and state
    GraphView.cs              # Instantiates and manages the visuals of the grid
    NodeView.cs               # Visual for a single node
    AStarSearch.cs            # A* algorithm implementation
    Snake.cs                  # Snake logic: movement, body tracking, food respawn, stuck recovery
    Heuristics.cs             # Computes Manhattan or Euclidean distances for the A* heuristic
    CameraPosition.cs         # Auto-positions the camera based on grid dimensions
  Models/
    tile.fbx                  # 3D tile mesh
    Materials/
      tileMat.mat             # Material applied to the tile mesh
  Prefab/
    NodeView.prefab           # Prefab instantiated by GraphView for each node
  Settings/                   # URP render pipeline and renderer assets
Packages/
  manifest.json               # Direct package dependencies
  packages-lock.json          # Locked dependency versions
ProjectSettings/              # Unity project configuration
```

---

## How It Works

- The grid is initialized as a flat 2D array of open nodes.
- The snake starts at position (0, 0). Food spawns at a random open position.
- A* runs continuously: each time the snake reaches food, food respawns, distances are recalculated, and A* restarts from the new head position.
- Node colors during simulation:
  - **Cyan** — open node
  - **Dark green** — snake head
  - **Green** — snake body
  - **Red** — food
  - **Magenta** — A* frontier nodes
  - **Grey** — A* explored nodes
  - **Cyan (path)** — current planned path
