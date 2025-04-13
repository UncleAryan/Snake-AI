using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Snake : MonoBehaviour {
    public Node head;
    private List<Node> body;


    public void init() {
        body = new List<Node>();
    }
}
