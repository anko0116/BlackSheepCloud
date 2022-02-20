using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    private Vector2 force;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        force = new Vector2(1000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ApplyForceBone();
        }
    }

    private void ApplyForceBone() {
        // Turn of rigidbody for player - that's the reason why player is not following weight
        // https://www.youtube.com/watch?v=5PVt29EgUhs&t=11s
        GameObject.Find("Player").transform.position = gameObject.transform.position;
        GameObject.Find("Player").transform.parent = gameObject.transform;
        body.AddForce(force);
    }
}
