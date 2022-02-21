using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    private Vector2 force;
    private Rigidbody2D body;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        force = new Vector2(1000, 0);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ApplyForceBone();
        }
        else {
            //player.GetComponent<Rigidbody2D>().isKinematic = false;    
        }
    }

    private void ApplyForceBone() {
        // Turn off rigidbody for player - that's the reason why player is not following weight
        // https://www.youtube.com/watch?v=5PVt29EgUhs&t=11s
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.transform.position = gameObject.transform.position;
        player.transform.parent = gameObject.transform;
        body.AddForce(force);
    }
}
