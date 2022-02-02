using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script attached to Player object
that shoots out raycast from Player, where these
raycasts will check for Shortcut objects.
If Shortcut is detected, Shortcut's script variable is modified.
*/

public class PlayerShortcut : MonoBehaviour
{
    private BoxCollider2D coll;
    private LayerMask mask;
    private float raycastLen;
    private Transform playerTransf;
    
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        mask = LayerMask.GetMask("Shortcut");
        raycastLen = 0.3f;
        playerTransf = GetComponent<Transform>();
    }


    void Update()
    {
        GameObject shortcut = DetectShortcutRight();
        if (shortcut != null) {
            shortcut.GetComponent<Shortcut>().leftCheck = true;   
        }
        shortcut = DetectShortcutLeft();
        if (shortcut != null) {
            shortcut.GetComponent<Shortcut>().rightCheck = true;
        }

    }
    private GameObject DetectShortcutRight() 
    {
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.right, coll.bounds.extents.y + raycastLen, mask);
        Debug.DrawRay(coll.bounds.center, Vector2.right * (coll.bounds.extents.y + raycastLen));
        if (hit.collider == null) {
            return null;
        }
        return hit.collider.gameObject;
    }
    private GameObject DetectShortcutLeft() 
    {
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.left, coll.bounds.extents.y + raycastLen, mask);
        Debug.DrawRay(coll.bounds.center, Vector2.left * (coll.bounds.extents.y + raycastLen));
        if (hit.collider == null) {
            return null;
        }
        return hit.collider.gameObject;
    }
}
