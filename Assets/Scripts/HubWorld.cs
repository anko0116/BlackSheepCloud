using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Attached to MainCamera prefab

public class HubWorld : MonoBehaviour
{
    public static bool activateHub = false;
    void Start()
    {
        Scene currScene = SceneManager.GetActiveScene();
        int sceneIndex = currScene.buildIndex;
        if (sceneIndex != 0) {
            activateHub = true;
        }
        else if (activateHub) {
            // Disable timer
            GameObject fogObj = GameObject.Find("FogCanvas").transform.Find("Fog").gameObject;
            fogObj.GetComponent<CalculatePixels>().disableTimer = true;

            // Disable timer UI
            Text timerText = GameObject.Find("TimerText").GetComponent<Text>();
            timerText.text = "";
        }
    }

    void Update()
    {
    }
}
