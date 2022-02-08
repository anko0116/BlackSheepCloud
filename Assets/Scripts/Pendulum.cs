using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Attached to Player

https://www.raywenderlich.com/348-make-a-2d-grappling-hook-game-in-unity-part-1
*/

public class Pendulum : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    void Start()
    {
        distanceJoint.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = gameObject.transform.position;
            mousePos.y += 0.5f;
            // Create visual line from clicked mouse position to the Player object
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, gameObject.transform.position);
            lineRenderer.enabled = true;
            // Create physical joint from Player object to the mouse position
            distanceJoint.connectedAnchor = mousePos; 
            distanceJoint.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            lineRenderer.enabled = false;
            distanceJoint.enabled = false;
        }

        if (distanceJoint.enabled) {
            lineRenderer.SetPosition(1, gameObject.transform.position);
        }
    }
}
