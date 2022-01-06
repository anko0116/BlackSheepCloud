using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;
    private BoxCollider2D coll;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private LayerMask mask;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        speed = 2;
        jumpSpeed = 3;
        mask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update() {
        /* If movement is only possible when player is grounded, then no way to contrl while mid-air.
        If movement is possible when NOT grounded, then player can shove itself to a wall
        */
        if (_IsGrounded()) {
            if (Input.GetKey(KeyCode.Space)) {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }
    }

    void FixedUpdate() {
        // Not grounded and not moving?
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
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
