using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class LaserScript : MonoBehaviour
{
    //public GameObject laserStart;
    //public GameObject laserMiddle;
    //private GameObject start;
    //private GameObject middle;
    //private Renderer ren;

    Laser laser;
    Hero hero;
    Enemy enemy;
    public TextMeshProUGUI text;
    public AudioClip laserSFX;
    public float maxLaserEnergy;
    public float currentLaserEnergy;
    public float depleteRate;
    public float increaseRate;
    float laserOffset = 40f;
    public int damage = 20;

    public float timeBetweenDamage = 1f;
    private float timeCounter;

    private void Start()
    {
        //ren = GetComponent<Renderer>();
        hero = FindObjectOfType<Hero>();
        laser = FindObjectOfType<Laser>();
        currentLaserEnergy = maxLaserEnergy;
        timeCounter = timeBetweenDamage;
    }

    void Update()
    {
        if (currentLaserEnergy < maxLaserEnergy)
        {
            currentLaserEnergy += Time.deltaTime * increaseRate;
        }
        if (Input.GetKey(KeyCode.Space) && currentLaserEnergy > 0 && hero.mode_tracker == 2)
        {
            timeCounter -= Time.deltaTime;
            laser.transform.position = hero.transform.position + new Vector3(0f, laserOffset, 1f);
            //AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, 0.5f);
            hero.SlowSpeed();

            if (timeCounter <= 0)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(transform.position - new Vector3(2.5f, 0, 0), transform.up, Mathf.Infinity);
                RaycastHit[] hits2;
                hits2 = Physics.RaycastAll(transform.position + new Vector3(2.5f, 0,0), transform.up, Mathf.Infinity);
                //raycast and also deal damage
                //Debug.Log(hits.Length);
                for (int i = 0; i < hits2.Length; i++)
                {
                    RaycastHit hit = hits2[i];                    
                    HandleHit(hit);
                    //do things to each object hit
                }
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    HandleHit(hit);
                }
                timeCounter = timeBetweenDamage;
            }
            
            currentLaserEnergy -= Time.deltaTime * depleteRate;

        }
        else
        {
            laser.transform.position = new Vector3(150, 0, 0);
            hero.FastSpeed();
        }
        text.text = currentLaserEnergy.ToString("F0");

    }

    private void HandleHit(RaycastHit hit)
    {
        Transform hitTransform = hit.transform;
        if (hitTransform.gameObject.GetComponent<Enemy>())
        {
            enemy = hitTransform.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        else
        {
            //Destroy(hitTransform.gameObject);
        }
    }
}