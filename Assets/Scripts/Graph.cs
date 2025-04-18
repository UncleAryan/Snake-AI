using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Graph : MonoBehaviour {
    public Node[,] nodes;

    int[,] mapData;
    public int mapWidth = -1;
    public int mapHeight = -1;

    // snake can only move in 4 directions (as in the original game)
    public static readonly Vector2[] allDirections = {
        new Vector2(0f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, -1f),
        new Vector2(-1f, 0f)
    };

    public void init(int[,] mapData) {
        this.mapData = mapData;
        mapWidth = mapData.GetLength(0);
        mapHeight = mapData.GetLength(1);
        nodes = new Node[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                NodeState nodeType = (NodeState)mapData[x, y];
                Node newNode = new Node(x, y, nodeType);
                nodes[x, y] = newNode;
                newNode.position = new Vector3(x, 0, y);
            }
        }

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                nodes[x, y].neighbors = getNeighbors(x, y, nodes);
            }
        }
    }

    public bool isInRange(int x, int y) {
        return (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight);
    }

    private List<Node> getNeighbors(int x, int y, Node[,] nodeArray) {
        List<Node> neighborNodes = new List<Node>();
        foreach (Vector2 d in allDirections) {
            int newX = x + (int)d.x; // new x for the direction we are looking in
            int newY = y + (int)d.y; // new y for the direction we are looking in

            if (isInRange(newX, newY) &&
                nodeArray[newX, newY] != null &&
                nodeArray[newX, newY].nodeState == NodeState.OPEN) {
                neighborNodes.Add(nodeArray[newX, newY]);
            }
        }
        return neighborNodes;
    }

    public void updateNeighbors() {
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                nodes[x, y].neighbors = getNeighbors(x, y, nodes);
            }
        }
    }
}
