using UnityEngine;

public class GameController : MonoBehaviour {
    public MapData mapData;
    public Graph graph;
    public CameraPosition cameraPosition;

    public float timeStep = 0.1f;
    
    void Start() {
        if(mapData != null && graph != null) {
            int[,] mapInstance = mapData.makeMap();
            graph.init(mapInstance);

            GraphView graphView = graph.GetComponent<GraphView>();
            if(graphView != null) {
                graphView.init(graph);
            } else {
                Debug.Log("GraphView is not attached to Graph");
            }

            // adjust the camera based upon the properties of the created map/grid
            if (cameraPosition != null) {
                cameraPosition.init(graph);
            }
        } else {
            Debug.Log("MapData or Graph is not attached to GameController");
        }
    }
}
