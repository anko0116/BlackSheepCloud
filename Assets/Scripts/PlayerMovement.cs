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

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        mask = LayerMask.GetMask("Ground");

        jumping = false;
    }

    void Update() {
        /* If movement is only possible when player is grounded, then no way to contrl while mid-air.
        If movement is possible when NOT grounded, then player can shove itself to a wall
        */
        if (_NewIsGrounded() && Input.GetKey(KeyCode.Space)) {
            jumping = true;
        }
        else {
            jumping = false;
        }
    }

    void FixedUpdate() {
        // Walking 
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        // Jumping
        if (jumping) body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        if (body.velocity.y < 0) {
            // if falling down
            body.velocity += Vector2.up * Physics2D.gravity.y * (0.8f - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (0.8f - 1) * Time.deltaTime;   
        }
        // if (jumping) body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private bool _IsGrounded() {
        float extraHeight = 0.02f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + extraHeight, mask);

        Color rayColor = Color.red;
        if (hit.collider != null) {
            rayColor = Color.green;
        }
        Debug.DrawRay(coll.bounds.center, Vector2.down * (coll.bounds.extents.y + extraHeight));

        return hit.collider != null;
    }

    private bool _NewIsGrounded() {
        return (body.velocity.y == 0 ? true : false);
    }

    // void OnCollisionStay(Collision collisionInfo)
    // {   
    //     print("HELLO");
    //     // Debug-draw all contact points and normals
    //     foreach (ContactPoint contact in collisionInfo.contacts)
    //     {
    //         print(contact.point);
    //         Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
    //     }
    // }
}

// 1. level 1 is hub world
// 2. hub world timer disappears when travelling to other maps
// 3. spawn in middle when exiting from other maps
// 4. spawn back in hub world when you get reset from other maps
// 5. cash-in widget at the beginning of hub world and in the middle of hub world
// 6. text above the first widget to tell player to cash-in "Transfer discovered pixels to time here by pressing F"