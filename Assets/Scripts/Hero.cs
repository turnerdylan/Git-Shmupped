﻿using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    static public Hero S; // Singleton                                    // a

    [Header("Set in the Unity Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("These fields are set dynamically")]
    [SerializeField]
    private float _shieldLevel = 1; // Remember to add the
                                    // This variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGo = null;                      // a

    void Awake()
    {
        S = this;  // Set the Singleton                                        // a
    }

    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");                             // b
        float yAxis = Input.GetAxis("Vertical");                               // b

        // Change transform.position basec on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic                        // c
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {                      // a
            TempFire();
        }
    }

    void TempFire()
    {                                                   // b
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: "+go.name);                                 // b
        // Make sure it's not the same triggering go as last time
        if (go == lastTriggerGo)
        {                                      // c
            return;
        }
        lastTriggerGo = go;                                             // d

        if (go.tag == "Enemy")
        {  // If the shield was triggered by an enemy
            shieldLevel--;        // Decrease the level of the shield by 1
            Destroy(go);          // ... and Destroy the enemy            // e
        }
        else
        {
            print("Triggered by non-Enemy:" + go.name);                  // f
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);                                     // a
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);                       // b
            // If the shield is going to be set to less than zero
            if (value < 0)
            {                                            // c
                Destroy(this.gameObject);
                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}