using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    BombScript bs;
    LaserScript ls;
    ShieldScript ss;
    Hero hero;
    BoundsCheck bc;
    public enum Type { health, shield, laser, bomb };
    public Type currentType;
    public int increaseAmt = 20;
    public int speed = 5;
    private int rotation;
    public AudioClip pickup;

    private void Start()
    {
        bc = GetComponent<BoundsCheck>();
        hero = FindObjectOfType<Hero>();
        bs = FindObjectOfType<BombScript>();
        ls = FindObjectOfType<LaserScript>();
        ss = FindObjectOfType<ShieldScript>();
        rotation = Random.Range(1, 4);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotation);
        MoveDown();
        if (bc != null && bc.offDown)
        {
            Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        Rigidbody rigidB = GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentType == Type.health)
        {
            hero.AddHealth();
        }
        else if (currentType == Type.shield)
        {
            AddShield();
        }
        else if (currentType == Type.laser)
        {
            AddLaser();
        }
        else if (currentType == Type.bomb)
        {
            AddBomb();
        }
        AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position, 0.5f);
    }

    private void AddBomb()
    {
        bs.currentBombEnergy += increaseAmt;
        if(bs.currentBombEnergy > 100)
        {
            bs.currentBombEnergy = 100;
        }
    }

    private void AddLaser()
    {
        ls.currentLaserEnergy += increaseAmt;
        if (ls.currentLaserEnergy > 100)
        {
            ls.currentLaserEnergy = 100;
        }
    }

    private void AddShield()
    {
        ss.currentShieldEnergy += increaseAmt;
        if (ss.currentShieldEnergy > 100)
        {
            ss.currentShieldEnergy = 100;
        }
    }
}
