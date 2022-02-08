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

    private SpriteRenderer spriteRend;
    private Sprite parasolSprite;
    private Sprite walkSprite;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        mask = LayerMask.GetMask("Ground");

        jumping = false;
        gliding = false;

        parasolSprite = Resources.Load("character/parasol2", typeof(Sprite)) as Sprite;
        walkSprite = Resources.Load("character/player2", typeof(Sprite)) as Sprite;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update() {
        jumping = false;
        gliding = false;
        if (_IsGrounded()) {
            if (Input.GetKey(KeyCode.W) && body.velocity.y == 0) {
                jumping = true;
            }
        }
        else if (Input.GetKey(KeyCode.W) && body.velocity.y <= 0) {
            gliding = true;
        }
    }

    void FixedUpdate() {
        // Walking 
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // Jumping
        if (jumping) body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        if (body.velocity.y < 0) {
            float fallFactor = 0.4f;
            if (gliding) {
                // Decrease falling factor
                fallFactor = 0.05f;
            }
            // if falling down
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0) {
            // if jumping up
            body.velocity += Vector2.up * Physics2D.gravity.y * (0.4f - 1) * Time.deltaTime;   
        }

        if (gliding) {
            // Change Player sprite to Parasol
            spriteRend.sprite = parasolSprite;
        }
        else {
            spriteRend.sprite = walkSprite;
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
}

// 1. level 1 is hub world
// 2. hub world timer disappears when travelling to other maps
// 3. spawn in middle when exiting from other maps
// 4. spawn back in hub world when you get reset from other maps
// 5. cash-in widget at the beginning of hub world and in the middle of hub world
// 6. text above the first widget to tell player to cash-in "Transfer discovered pixels to time here by pressing F"
// 7. cone torch (move with mouse)

// Spiderman stick to ceiling