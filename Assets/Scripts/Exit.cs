using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Exit : MonoBehaviour
{
    public int nextSceneIndex; // Set in the Inspector for each Exit

    private Transform playerTransf;
    private Vector2 exitPos;

    void Start() {
        playerTransf = GameObject.Find("Player").GetComponent<Transform>();
        exitPos = GetComponent<Transform>().position;
    }

    void Update() {
        Vector2 playerPos = playerTransf.position;
        if (Math.Abs(playerPos.x - exitPos.x) < 0.05f && Math.Abs(playerPos.y - exitPos.y) < 0.06f) {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
