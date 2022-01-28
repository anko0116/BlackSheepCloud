using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attached to MainCamera prefab

public class HubWorld : MonoBehaviour
{
    public static bool activateHub = false;
    void Start()
    {
        print("START");
        Scene currScene = SceneManager.GetActiveScene();
        int sceneIndex = currScene.buildIndex;
        if (sceneIndex != 0) {
            activateHub = true;
        }
        else if (activateHub) {
            GameObject fogObj = GameObject.Find("FogCanvas").transform.Find("Fog").gameObject;
            fogObj.GetComponent<CalculatePixels>().disableTimer = true;
        }
    }

    void Update()
    {
        print(activateHub);
    }
}
