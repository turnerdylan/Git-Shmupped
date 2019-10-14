using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    Hero hero;
    public TextMeshProUGUI text;
    public GameObject bulletPrefab;
    public float projectileSpeed;
    public float maxBulletEnergy;
    private float currentBulletEnergy;
    public float depleteRate;
    public float increaseRate;

    public float timeBetweenShots = 0.5f;
    private float timeCounter;

    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        currentBulletEnergy = maxBulletEnergy;
        timeCounter = timeBetweenShots;
    }

    public void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(bulletPrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
        timeCounter = timeBetweenShots;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;
         //increases energy over time
        if (currentBulletEnergy < maxBulletEnergy)
        {
            currentBulletEnergy += Time.deltaTime * increaseRate;
        }
        //checks if youre in the right mode
        if (hero.mode_tracker == 2)
        {
            //checks if youre pressing space and have energy to spend and checks firerate
            if (Input.GetKey(KeyCode.Space) && currentBulletEnergy > 0)
            {
                if (timeCounter <= 0)
                {
                    TempFire();
                }
                currentBulletEnergy -= Time.deltaTime * depleteRate;
            }
        }
        text.text = currentBulletEnergy.ToString("F0");
    }
}
