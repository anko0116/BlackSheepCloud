using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCheat : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha1)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha3)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha4)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha5)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(4);
            }
        }
    }
}
