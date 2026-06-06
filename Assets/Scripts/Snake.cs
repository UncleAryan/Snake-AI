using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    public AStarSearch aStarSearch;
    public Heuristics heuristics;
    public Color headColor;
    public Color foodColor;
    public Color bodyColor;
    public Color openColor = Color.cyan;
    

    public Node head;
    public Node food;
    public List<Node> body;
    private Graph graph;
    private GraphView graphView;

    public void init(Graph graph, GraphView graphView, Node head, Node food) {
        body = new List<Node>();
        if (graph != null && graphView != null && head != null && food != null) {
            this.graph = graph;
            this.graphView = graphView;
            this.head = head;
            this.food = food;
        } else {
            Debug.Log("Graph, GraphView, Head, or Food is null in Snake");
        }

        showColors();
    }

    public IEnumerator moveHeadToFood(List<Node> path, float timeStep) {
        Node oldHeadPosition = head;

        foreach (Node node in path) {
            oldHeadPosition = head;

            head = node;
            oldHeadPosition.nodeState = NodeState.OPEN;

            if (body.Count > 0) {
                graph.nodes[body[body.Count - 1].xIndex, body[body.Count - 1].yIndex].nodeState = NodeState.OPEN;

                for (int i = body.Count - 1; i > 0; i--) {
                    body[i] = body[i - 1];
                }
                body[0] = oldHeadPosition;
                body[0].nodeState = NodeState.BODY;
            }

            showColors();
            yield return new WaitForSeconds(timeStep);

           
        }

        if (head == food) {
            if (body.Count > 0) {
                body.Add(body[body.Count - 1]);
                body[body.Count - 1].nodeState = NodeState.BODY;
            } else {
                body.Add(oldHeadPosition);
                oldHeadPosition.nodeState = NodeState.BODY;
            }
        }

        foreach (Node bodyNode in body) {
            bodyNode.nodeState = NodeState.BODY;
        }


        food = findRandomOpenNode();
        heuristics.init(graph, food);
        graphView.updateDistanceDisplay();
        graph.updateNeighbors();
        aStarSearch.reset(head, food);
        StartCoroutine(aStarSearch.searchRoutine(timeStep));
    }

    public IEnumerator createSpace(float timeStep) {
        Node oldHeadPosition = head;

        foreach (Node node in head.neighbors) {
            oldHeadPosition = head;

            head = node;
            oldHeadPosition.nodeState = NodeState.OPEN;

            if (body.Count > 0) {
                graph.nodes[body[body.Count - 1].xIndex, body[body.Count - 1].yIndex].nodeState = NodeState.OPEN;

                for (int i = body.Count - 1; i > 0; i--) {
                    body[i] = body[i - 1];
                }
                body[0] = oldHeadPosition;
                body[0].nodeState = NodeState.BODY;
            }

            showColors();
            yield return new WaitForSeconds(timeStep);
        }

        graph.updateNeighbors();
        aStarSearch.reset(head, food);
        StartCoroutine(aStarSearch.searchRoutine(timeStep));
    }

    private Node findRandomOpenNode() {
        int x = Random.Range(0, graph.mapWidth);
        int y = Random.Range(0, graph.mapHeight);
        Node randomOpenNode = graph.nodes[x, y];

        while (randomOpenNode.nodeState != NodeState.OPEN) {
            x = Random.Range(0, graph.mapWidth);
            y = Random.Range(0, graph.mapHeight);
            randomOpenNode = graph.nodes[x, y];
        }

        return randomOpenNode;
    }

    private void showColors(GraphView graphView, Node start, Node goal) {
        foreach (NodeView node in graphView.nodeViews) {
            node.colorNode(openColor);
        }

        NodeView startNodeView = graphView.nodeViews[start.xIndex, start.yIndex];
        NodeView goalNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex];

        if (startNodeView != null) {
            startNodeView.colorNode(headColor);
        }

        if (goalNodeView != null) {
            goalNodeView.colorNode(foodColor);
        }

        foreach (Node segment in body) {
            NodeView bodyNodeView = graphView.nodeViews[segment.xIndex, segment.yIndex];
            if (bodyNodeView != null) {
                bodyNodeView.colorNode(bodyColor); 
            }
        }
    }

    private void showColors() {
        showColors(graphView, head, food);
    }
}
