using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (0 != SceneManager.GetActiveScene().buildIndex) {
            this.gameObject.SetActive(false);
        }
        else {
            this.gameObject.SetActive(true);
        }
    }
}
