using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombScript : MonoBehaviour
{
    Hero hero;
    public TextMeshProUGUI text;
    public GameObject bombPrefab;
    public float projectileSpeed;
    public float maxBombEnergy;
    public float currentBombEnergy;
    public float depleteRate;
    public float increaseRate;

    public float timeBetweenShots = 0.5f;
    private float timeCounter;

    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        currentBombEnergy = maxBombEnergy;
        timeCounter = timeBetweenShots;
    }

    public void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(bombPrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
        timeCounter = timeBetweenShots;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;
        //increases energy over time
        if (currentBombEnergy < maxBombEnergy)
        {
            currentBombEnergy += Time.deltaTime * increaseRate;
        }
        //checks if youre in the right mode
        if (hero.mode_tracker == 3)
        {
            //checks if youre pressing space and have energy to spend and checks firerate
            if (Input.GetKey(KeyCode.Space) && currentBombEnergy > 0)
            {
                if (timeCounter <= 0)
                {
                    TempFire();
                }
                currentBombEnergy -= Time.deltaTime * depleteRate;
            }
        }
        text.text = currentBombEnergy.ToString("F0");
    }
}
