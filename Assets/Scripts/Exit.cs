using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Exit : MonoBehaviour
{
    private string sceneName;
    private int sceneIndex;
    private Transform playerTransf;
    private Vector2 exitPos;

    private float timer;

    void Start() {
        Scene currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
        sceneIndex = currScene.buildIndex;

        playerTransf = GameObject.Find("Player").GetComponent<Transform>();
        exitPos = GetComponent<Transform>().position;
    }

    void Update() {
        Vector2 playerPos = playerTransf.position;
        if (Math.Abs(playerPos.x - exitPos.x) < 0.05f && Math.Abs(playerPos.y - exitPos.y) < 0.06f) {
            if (sceneIndex == 4) return;
            SceneManager.LoadScene(sceneIndex+1 % 5);
            return;
        }
    }
}
