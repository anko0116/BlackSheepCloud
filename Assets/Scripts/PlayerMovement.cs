using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;
    private BoxCollider2D coll;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private LayerMask mask;

    private bool jumping;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        speed = 1.2f;
        jumpSpeed = 3.6f;
        mask = LayerMask.GetMask("Ground");

        jumping = false;
    }

    void Update() {
        /* If movement is only possible when player is grounded, then no way to contrl while mid-air.
        If movement is possible when NOT grounded, then player can shove itself to a wall
        */
        if (_IsGrounded() && Input.GetKey(KeyCode.Space)) {
            jumping = true;
        }
        else {
            jumping = false;
        }
    }

    void FixedUpdate() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if (jumping) body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        // Code below seems to add force at wrong times due to race conditions
        // if (jumping) {
        //     jumping = false;
        //     body.AddForce(new Vector2(body.velocity.x, jumpSpeed), ForceMode2D.Impulse);
        // }
    }

    private bool _IsGrounded() {
        float extraHeight = 0.04f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + extraHeight, mask);

        Color rayColor = Color.red;
        if (hit.collider != null) {
            rayColor = Color.green;
        }
        Debug.DrawRay(coll.bounds.center, Vector2.down * (coll.bounds.extents.y + extraHeight));

        return hit.collider != null;
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

// 1. Stop physics engine when time resets
// 2. Doublecheck collisions on the map
// 3. memory leak
// 4. torch opacity