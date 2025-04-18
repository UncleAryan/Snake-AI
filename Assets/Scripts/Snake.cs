using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Snake : MonoBehaviour {
    public AStarSearch aStarSearch;
    public Heuristics heuristics;
    public Color headColor;
    public Color foodColor;
    public Color openColor = Color.cyan;
    

    public Node head;
    public Node food;
    private Graph graph;
    private GraphView graphView;

    public void init(Graph graph, GraphView graphView, Node head, Node food) {
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
        Node previousHeadNode = head;
        foreach(Node node in path) {
            head = node;
            previousHeadNode.nodeState = NodeState.OPEN;
            showColors();
            yield return new WaitForSeconds(timeStep);
        }

        Node previousFoodNode = food;
        food = findRandomOpenNode();
        previousHeadNode.nodeState = NodeState.OPEN;

        heuristics.init(graph, food);
        graphView.updateDistanceDisplay();

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
    }

    private void showColors() {
        showColors(graphView, head, food);
    }
}
