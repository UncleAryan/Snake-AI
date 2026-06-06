using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour {
    public GameObject nodeViewPrefab;
    public Color openColor = Color.cyan;
    public Color headColor = new Color(0f, 0.3f, 0f);
    public Color bodyColor = Color.green;
    public Color foodColor = Color.red;
    public NodeView[,] nodeViews;

    private Graph graph;

    public void init(Graph graph) {
        this.graph = graph; 
        nodeViews = new NodeView[graph.mapWidth, graph.mapHeight];

        // go through each node in the nodes array in Graph class
        foreach (Node node in graph.nodes) {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if (nodeView != null) {
                nodeView.init(node);
                nodeViews[node.xIndex, node.yIndex] = nodeView;
                if (node.nodeState == NodeState.OPEN) {
                    nodeView.colorNode(openColor);
                } else if (node.nodeState == NodeState.HEAD) {
                    nodeView.colorNode(headColor);
                } else if (node.nodeState == NodeState.BODY) {
                    nodeView.colorNode(bodyColor);
                } else {
                    nodeView.colorNode(foodColor);
                }
            }
        }
    }

    public void updateDistanceDisplay() {
        for (int x = 0; x < nodeViews.GetLength(0); x++) {
            for (int y = 0; y < nodeViews.GetLength(1); y++) {
                nodeViews[x, y].updateDistance(graph.nodes[x, y]);
            }
        }
    }

    public void colorNodes(List<Node> nodes, Color color) {
        foreach (Node node in nodes) {
            if (node != null) {
                NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
                if (nodeView != null) {
                    nodeView.colorNode(color);
                }
            }
        }
    }
}
