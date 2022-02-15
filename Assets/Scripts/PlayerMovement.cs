using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
// Better Jumping in Unity With Four Lines of Code
// https://www.youtube.com/watch?v=7KiK0Aqtmzc
public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float jumpSpeed;

    private Rigidbody2D body;
    private BoxCollider2D coll;
    private LayerMask mask;

    private bool jumping;
    private bool gliding;
    private bool swinging;

    private SpriteRenderer spriteRend;
    private Sprite parasolSprite;
    private Sprite walkSprite;
    private Sprite swingSprite;

    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        mask = LayerMask.GetMask("Ground");

        jumping = false;
        gliding = false;
        swinging = false;

        parasolSprite = Resources.Load("character/parasol2", typeof(Sprite)) as Sprite;
        walkSprite = Resources.Load("character/player2", typeof(Sprite)) as Sprite;
        swingSprite = Resources.Load("character/swing2", typeof(Sprite)) as Sprite;
        spriteRend = GetComponent<SpriteRenderer>();

        distanceJoint.enabled = false;
    }

    void Update() {
        jumping = false;
        gliding = false;
        // swinging = false;

        if (Input.GetKeyDown(KeyCode.Space)) {
            swinging = true;
            // Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 endPos = gameObject.transform.position;
            endPos.y += 0.3f;
            // Create visual line from clicked mouse position to the Player object
            lineRenderer.SetPosition(0, endPos);
            lineRenderer.SetPosition(1, gameObject.transform.position);
            //lineRenderer.enabled = true;
            // Create physical joint from Player object to the mouse position
            distanceJoint.connectedAnchor = endPos; 
            distanceJoint.enabled = true;

            // Change RigidBody2D to Static (turn off)
            body.bodyType = RigidbodyType2D.Static;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            swinging = false;
            lineRenderer.enabled = false;
            distanceJoint.enabled = false;
            // Change RigidBody2D to Dynamic
            body.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (_IsGrounded()) {
            if (Input.GetKey(KeyCode.W) && body.velocity.y == 0) {
                jumping = true;
            }
        }
        else if (Input.GetKey(KeyCode.W) && body.velocity.y <= 0) {
            gliding = true;
        }

        if (swinging && Input.GetKey(KeyCode.D)) {
            // Move swing right

        }

        ChangeSprite();
    }

    void FixedUpdate() {
        // Walking
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // Jumping
        if (jumping) body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        if (!swinging && body.velocity.y < 0) {
            float fallFactor = 0.4f;
            if (gliding) {
                // Decrease falling factor
                fallFactor = 0.03f;
            }
            // if falling down
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
        }
        else if (!swinging && body.velocity.y > 0) {
            // if jumping up
            body.velocity += Vector2.up * Physics2D.gravity.y * (0.4f - 1) * Time.deltaTime;   
        }

        // When swinging, force Player down faster
        if (swinging) {
            // float midX = lineRenderer.GetPosition(0).x;
            // if (transform.position.x > midX) {
            //     Vector2 addVel = new Vector2(0, 0);
            //     //addVel.y = (Physics2D.gravity.y * (2f - 1) * Time.deltaTime) / (-1 * body.velocity.y + 0.1f);
            //     addVel.x = -0.2f * (transform.position.x - midX);
            //     body.velocity += addVel;
            // }
            // else if (transform.position.x < midX) {
            //     Vector2 addVel = new Vector2(0, 0);
            //     //addVel.y = (Physics2D.gravity.y * (2f - 1) * Time.deltaTime) / (-1 * body.velocity.y + 0.1f);
            //     addVel.x = 0.2f * (midX - transform.position.x);
            //     body.velocity += addVel;
            // }
            // body.velocity += new Vector2(0,1);

            transform.position += new Vector3(0,1,0) * Time.deltaTime;
        }
    }

    private bool _IsGrounded() {
        float extraHeight = 0.02f;
        Vector2 hit1Origin = coll.bounds.center;
        Vector2 hit2Origin = new Vector2(hit1Origin.x + 0.023f, hit1Origin.y - 0.01f);
        Vector2 hit3Origin = new Vector2(hit1Origin.x - 0.023f, hit1Origin.y - 0.01f);
        RaycastHit2D hit1 = Physics2D.Raycast(hit1Origin, Vector2.down, coll.bounds.extents.y + extraHeight, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(hit2Origin, Vector2.down, coll.bounds.extents.y + extraHeight, mask);
        RaycastHit2D hit3 = Physics2D.Raycast(hit3Origin, Vector2.down, coll.bounds.extents.y + extraHeight, mask);

        Debug.DrawRay(coll.bounds.center, Vector2.down * (coll.bounds.extents.y + extraHeight));
        Debug.DrawRay(hit2Origin, Vector2.down * (coll.bounds.extents.y + extraHeight));
        Debug.DrawRay(hit3Origin, Vector2.down * (coll.bounds.extents.y + extraHeight));

        return (hit1.collider != null || hit2.collider != null || hit3.collider != null);
    }

    private void ChangeSprite() {
        // Change Sprite of Player
        if (swinging) {
            spriteRend.sprite = swingSprite;
        }
        else if (gliding) {
            spriteRend.sprite = parasolSprite;
        }
        else {
            spriteRend.sprite = walkSprite;
        }
    }
}

// 1. level 1 is hub world
// 2. hub world timer disappears when travelling to other maps
// 3. spawn in middle when exiting from other maps
// 4. spawn back in hub world when you get reset from other maps
// 5. cash-in widget at the beginning of hub world and in the middle of hub world
// 6. text above the first widget to tell player to cash-in "Transfer discovered pixels to time here by pressing F"
// 7. cone torch (move with mouse)

// Spiderman stick to ceiling