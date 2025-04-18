using TMPro;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class NodeView: MonoBehaviour {
    public GameObject tile;
    [Range(0, 0.5f)]
    public float borderSize = 0.15f;
    GameObject textObject;
    TextMeshPro textMeshPro;

    public void init(Node node) {
        if (tile != null) {
            tile.name = "Node (" + node.position.x + ", " + node.position.z + ")";
            tile.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f, 1f - borderSize);
        }

        displayDistance(node);
    }

    private void colorNode(Color color, GameObject gameObject) {
        if (gameObject != null) {
            Renderer gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObjectRenderer.material.color = color;
        }
    }

    public void colorNode(Color color) {
        colorNode(color, tile);
    }

    public void updateDistance(Node node) {
        if (node.nodeState == NodeState.OPEN && (Heuristics.StaticHeuristics.manhattanStaticBool || Heuristics.StaticHeuristics.euclideanStaticBool)) {
            textMeshPro.text = node.distance.ToString();
        }
    }

    public void displayDistance(Node node) {
        if (node.nodeState == NodeState.OPEN && (Heuristics.StaticHeuristics.manhattanStaticBool || Heuristics.StaticHeuristics.euclideanStaticBool)) {
            textObject = new GameObject("Distance");
            textObject.transform.SetParent(tile.transform);
            textMeshPro = textObject.AddComponent<TextMeshPro>();
            textMeshPro.alignment = TextAlignmentOptions.Center;
            textMeshPro.fontSize = 8;
            textMeshPro.color = Color.black;

            textMeshPro.transform.localPosition = new Vector3(0, 0.1f, 0);
            textMeshPro.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            textMeshPro.text = node.distance.ToString();
        }
    }
}
