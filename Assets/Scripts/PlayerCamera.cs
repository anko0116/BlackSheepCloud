using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// https://www.youtube.com/watch?time_continue=82&v=YP723zBCXfk&feature=emb_logo count pixels
// https://www.youtube.com/watch?v=MUV9Nr-cIGU
public class PlayerCamera : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;
    [SerializeField] private float maxTop;
    [SerializeField] private float maxBottom;
    private Camera cam;
    void Start()
    {
        player = GameObject.Find("player2").transform;
        maxLeft = player.position.x;
        maxRight = player.position.x;
        maxTop = player.position.y;
        maxBottom = player.position.y;

        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Update max values
        Vector2 playerPos = player.position;
        maxLeft = playerPos.x < maxLeft ? playerPos.x : maxLeft;
        maxRight = maxRight < playerPos.x ? playerPos.x : maxRight;
        maxTop = playerPos.y > maxTop ? playerPos.y : maxTop;
        maxBottom = maxBottom > playerPos.y ? playerPos.y : maxBottom;

        // Set camera position
        float width = maxLeft + maxRight;
        float height = maxTop + maxBottom;
        transform.position = new Vector3(width / 2, height / 2, -10f);

        // Set camera orthographic size
        float absWidth = Math.Abs(maxLeft - maxRight);
        float absHeight = Math.Abs(maxTop - maxBottom);
        cam.orthographicSize = absWidth > absHeight ? (absWidth / 2.5f) + 0.5f : (absHeight / 2.5f) + 0.5f;
    }

    private Vector4 CalculateCameraPos() {
        return new Vector4();
    }
}
