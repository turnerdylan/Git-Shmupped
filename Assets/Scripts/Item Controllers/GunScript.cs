using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    Hero hero;
    public AudioClip laserSFX;
    public GameObject bulletPrefab;
    public float projectileSpeed;

    public float timeBetweenShots = 0.5f;
    private float timeCounter;

    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        timeCounter = timeBetweenShots;
    }

    public void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(bulletPrefab);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, 0.5f);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
        timeCounter = timeBetweenShots;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;
        //checks if youre in the right mode
        if (hero.mode_tracker == 0)
        {
            //checks if youre pressing space and have energy to spend and checks firerate
            if (Input.GetKey(KeyCode.Space) && timeCounter <= 0)
            {
                TempFire();
            }
        }
    }
}
