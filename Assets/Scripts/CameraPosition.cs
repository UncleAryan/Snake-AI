using UnityEngine;

/*
 * This class is used to auto adjust the world transform 
 * properties of the main camera so that the user can 
 * change the map width and height without having to 
 * worry about re-adjusting the camera properties to
 * fit the new grid on the game scene.
 */
public class CameraPosition : MonoBehaviour {
    public void init(Graph graph) {
        Camera mainCamera = Camera.main;

        if (graph != null && mainCamera != null) {
            mainCamera.transform.position = new Vector3(graph.mapWidth / 2, (float)(graph.mapHeight + graph.mapHeight * 0.5), graph.mapHeight / 2);
            mainCamera.transform.eulerAngles = new Vector3(90, 0, 0); // it will always rotate 90 degrees
            mainCamera.transform.localScale = new Vector3(1, 1, 1); // default setting
        } else {
            Debug.Log("The graph or main camera is null in CameraPosition.");
        } 
    }
}
