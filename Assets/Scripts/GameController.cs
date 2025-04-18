using UnityEngine;

public class GameController : MonoBehaviour {
    public MapData mapData;
    public Graph graph;
    public CameraPosition cameraPosition;
    public Snake snake;
    public Heuristics heuristics;
    public AStarSearch aStarSearch;
    public int startX;
    public int startY;
    public int goalX;
    public int goalY;
    public float timeStep = 0.1f;
    
    void Start() {
        if(mapData != null && graph != null) {
            int[,] mapInstance = mapData.makeMap();
            graph.init(mapInstance);

            startX = 0;
            startY = 0;
            goalX = Random.Range(1, graph.mapWidth); //  avoid the first row so it does not interfere with the startNode
            goalY = Random.Range(1, graph.mapHeight); //  avoid the first col so it does not interfere with the startNode

            Node startNode = graph.nodes[startX, startY];
            Node goalNode = graph.nodes[goalX, goalY];

            GraphView graphView = graph.GetComponent<GraphView>();
            if (graphView != null && heuristics != null) {
                heuristics.init(graph, goalNode);
                graphView.init(graph);
            } else {
                Debug.Log("Error");
            }

            // adjust the camera based upon the properties of the created map/grid
            if (cameraPosition != null) {
                cameraPosition.init(graph);
            } else {
                Debug.Log("CameraPosition is null in GameController");
            }

            if (snake != null) {
                snake.init(graph, graphView, startNode, goalNode);
            } else {
                Debug.Log("Snake is null in GameController");
            }

            if (aStarSearch != null) {
                aStarSearch.init(graph, graphView, snake.head, snake.food);
                StartCoroutine(aStarSearch.searchRoutine(timeStep));
            } else {
                Debug.Log("AStarSearch is null in GameController");
            }
        } else {
            Debug.Log("MapData or Graph is not attached to GameController");
        }
    }

}
