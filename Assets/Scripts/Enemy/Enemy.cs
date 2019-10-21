using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Specs")]
    public float speed = 10f;
    public float health = 100;
    public int score = 100;      // Points earned for destroying this
    int collisionDamage = 20;
    int chanceForDrop = 50;
    Animator anim;
    Hero hero;
    public GameObject[] powerups;

    private BoundsCheck bndCheck;                                       // a

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        anim = GetComponent<Animator>();
        hero = FindObjectOfType<Hero>();
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {                                                // a
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {

        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "ProjectileHero")
        {
            Destroy(coll.gameObject);  // Destroy the Projectile
            //Destroy(gameObject);   // Destroy this Enemy GameObject
            if (coll.gameObject.GetComponent<DamageDealer>())
            {
                //deal damage to enemy
                int damage = coll.gameObject.GetComponent<DamageDealer>().damageValue;
                TakeDamage(damage);
            }
        }
        else if (coll.tag == "Hero")
        {
            hero.TakeDamage(collisionDamage);
            Destroy(gameObject);
        }
        else if (coll.tag == "Bomb")
        {
            coll.gameObject.GetComponent<DamageDealer>();
            int damage = coll.gameObject.GetComponent<DamageDealer>().damageValue;
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int dmg)
    {
        //do animaitons, damage, and potentially destroy
        anim.SetTrigger("onHit");
        health -= dmg;
        if(health <= 0 && !anim.GetBool("Exploded"))
        {
            /*//start anim
            anim.SetBool("Exploded", true);
            StartCoroutine(DestroyEnemy());*/
            hero.AddScore(score);
            int diceRoll = Random.Range(0, 100);
            if(diceRoll <= chanceForDrop)
            {
                int index = Random.Range(0, 4);
                GameObject drop = GameObject.Instantiate(powerups[index]) as GameObject;
                drop.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }

    //used for animation delay
    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }

    
}