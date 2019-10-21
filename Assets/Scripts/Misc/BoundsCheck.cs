using UnityEngine;
using System.Collections;

// To type the next 4 lines, start by typing /// and then Tab.
/// <summary>
/// 
/// /// Checks whether a GameObject is on screen and can force it to stay on screen.
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera.
/// </summary>
public class BoundsCheck : MonoBehaviour
{                             // a
    [Header("Set in the Unity Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;                                // a

    [Header("These fields are set dynamically")]
    public bool isOnScreen = true;                                  // b
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;                  // a

    void Start()
    {
        camHeight = Camera.main.orthographicSize;                      // b
        camWidth = camHeight * Camera.main.aspect;                     // c
    }

    void LateUpdate()
    {                                               // d
        Vector3 pos = transform.position;
        isOnScreen = true;                                              // d
        offRight = offLeft = offUp = offDown = false;                   // b

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
            isOnScreen = false;                                         // e
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
            isOnScreen = false;                                         // e
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
            isOnScreen = false;                                         // e
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
            isOnScreen = false;                                         // e
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);        // d
        if (keepOnScreen && !isOnScreen)
        {                            // f
            transform.position = pos;                                   // g
            isOnScreen = true;
        }

    }
}