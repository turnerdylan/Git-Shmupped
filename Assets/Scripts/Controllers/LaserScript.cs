using UnityEngine;
using System.Collections;
using TMPro;

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
    public float maxLaserEnergy;
    private float currentLaserEnergy;
    public float depleteRate;
    public float increaseRate;
    float laserOffset = 40f;
    public int damage = 20;
    public float slowSpeed = 15;

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
        timeCounter -= Time.deltaTime;
        if (currentLaserEnergy < maxLaserEnergy)
        {
            currentLaserEnergy += Time.deltaTime * increaseRate;
        }
        if (Input.GetKey(KeyCode.Space) && currentLaserEnergy > 0 && hero.mode_tracker == 3)
        {
            laser.transform.position = hero.transform.position + new Vector3(0f, laserOffset, 1f);
            
            hero.speed = slowSpeed;
            Debug.Log("firing laser   " + hero.speed);

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
                    Transform hitTransform = hit.transform;
                    if (hitTransform.gameObject.GetComponent<Enemy>())
                    {
                        enemy = hitTransform.gameObject.GetComponent<Enemy>();
                        enemy.TakeDamage(damage);
                    }
                    //do things to each object hit
                }
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    Transform hitTransform = hit.transform;
                    if (hitTransform.gameObject.GetComponent<Enemy>())
                    {
                        enemy = hitTransform.gameObject.GetComponent<Enemy>();
                        enemy.TakeDamage(damage);
                    }
                }
                timeCounter = timeBetweenDamage;
            }
            
            currentLaserEnergy -= Time.deltaTime * depleteRate;

        }
        else
        {
            laser.transform.position = new Vector3(150, 0, 0);
        }
        text.text = currentLaserEnergy.ToString("F0");

    }
}