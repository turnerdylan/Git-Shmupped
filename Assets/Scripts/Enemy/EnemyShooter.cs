using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AudioClip shootSFX;
    public float projectileSpeed;
    Vector3 moveDir;
    Hero hero;

    public float timeBetweenShots = 3f;
    private float timeCounter;

    public enum Type { Straight, AtHero, Random };
    public Type currentType;

    private void Start()
    {
        timeCounter = timeBetweenShots;
        hero = FindObjectOfType<Hero>();
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;
        if(timeCounter <= 0)
        {
            if(currentType == Type.Straight)
            {
                FireStraight();
            } else if (currentType == Type.AtHero)
            {
                FireAtHero();
            }
            timeCounter = timeBetweenShots;
        }  
    }

    private void FireAtHero()
    {
        if(hero)
        {
            Vector3 heroPos = hero.transform.position;
            GameObject projGO = Instantiate(bulletPrefab);
            PlaySound();
            projGO.transform.position = transform.position;
            //projGO.transform.Rotate(new Vector3(0, 0, 20f));
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            moveDir = (heroPos - transform.position).normalized * projectileSpeed;
            rigidB.velocity = new Vector3(moveDir.x, moveDir.y);
        }
    }

    private void FireStraight()
    {
        GameObject projGO = Instantiate<GameObject>(bulletPrefab);
        PlaySound();
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * projectileSpeed;
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, 0.5f);
    }


}
