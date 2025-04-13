using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeState {
    HEAD = 0,
    BODY = 1,
    FOOD = 2,
    OPEN = 3
}
public class Node {
    public NodeState nodeState;
    public int xIndex;
    public int yIndex;
    public Vector3 position;
    public List<Node> neighbors;

    public Node(int x, int y, NodeState state) {
        xIndex = x; 
        yIndex = y;
        nodeState = state;
        neighbors = new List<Node>();
    }
}
