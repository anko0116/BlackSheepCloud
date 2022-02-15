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

    private SpriteRenderer spriteRend;
    private Sprite swingSprite;
    private Sprite walkSprite;

    void Start()
    {
        distanceJoint.enabled = false;

        spriteRend = GetComponent<SpriteRenderer>();
        swingSprite = Resources.Load("character/swing2", typeof(Sprite)) as Sprite;
        walkSprite = Resources.Load("character/player2", typeof(Sprite)) as Sprite;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = gameObject.transform.position;
            mousePos.y += 0.3f;
            // Create visual line from clicked mouse position to the Player object
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, gameObject.transform.position);
            lineRenderer.enabled = true;
            // Create physical joint from Player object to the mouse position
            distanceJoint.connectedAnchor = mousePos; 
            distanceJoint.enabled = true;
            print(swingSprite);
            spriteRend.sprite = swingSprite;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            lineRenderer.enabled = false;
            distanceJoint.enabled = false;

            spriteRend.sprite = walkSprite;
        }

        if (distanceJoint.enabled) {
            lineRenderer.SetPosition(1, gameObject.transform.position);
        }
    }
}
