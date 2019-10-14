using UnityEngine;                 // Required for Unityusing System.Collections;          // Required for Arrays & other Collections

public class Enemy : MonoBehaviour
{
    [Header("Enemy Specs")]
    public float speed = 10f;      // The speed in m/s
    //public float fireRate = 0.3f;  // Seconds/shot (Unused)
    public float health = 100;
    public int score = 100;      // Points earned for destroying this
    int collisionDamage = 20;
    Animator anim;
    Hero hero;

    private BoundsCheck bndCheck;                                       // a

    void Awake()
    {                                                      // b
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

        Move();
        //transform.Rotate(new Vector3(0, 0, 2f));

        if (bndCheck != null && bndCheck.offDown)
        {                   // a
            // We're off the bottom, so destroy this GameObject         // b
            Destroy(gameObject);                                      // b
        }
    }


    public virtual void Move()
    {                                        // b
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime; pos = tempPos;
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
        else
        {
            print("Enemy hit by non-ProjectileHero: " + coll.name);
        }

        
    }

    public void TakeDamage(int dmg)
    {
        //do animaitons, damage, and potentially destroy
        anim.SetTrigger("onHit");
        health -= dmg;
        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }
}