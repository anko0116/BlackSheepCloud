using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
    public bool leftCheck;
    public bool rightCheck;

    void Start()
    {
        leftCheck = false;
        rightCheck = false;
    }

    void Update()
    {
        if (leftCheck && rightCheck) {
            gameObject.SetActive(false);
        }
    }
}
