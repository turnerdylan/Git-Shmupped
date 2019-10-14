using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

public class Hero : MonoBehaviour
{
    static public Hero S;
    Animator anim;
    ShieldScript shield;
    ScreenShake shake;

    [Header("Player specs")]
    public float speed = 30;
    public float playerHealth;

    //public float gameRestartDelay = 2f;

    [Header("UI mode controls")]
    public TextMeshProUGUI tmp;
    string[] modes = { "shield", "gun", "laser", "bomb" };
    public int mode_tracker = 1;

    private void Start()
    {
        shield = FindObjectOfType<ShieldScript>();
        anim = GetComponent<Animator>();
        shake = FindObjectOfType<ScreenShake>();
        tmp.text = "bomb";
    }

    void Awake()
    {
        S = this;
    }

    void Update()
    {
        //get input and move the player around
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        Debug.Log(speed);
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //do player animations
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isLeft", true);
        }
        else
        {
            anim.SetBool("isLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRight", true);
        }
        else
        {
            anim.SetBool("isRight", false);
        }

        //change modes of the ship
        if (Input.GetKeyDown("e"))
        {
            tmp.text = modes[mode_tracker];
            if (mode_tracker == 3)
            {
                mode_tracker = 0;
            }
            else
            {
                mode_tracker++;
            }
            //Debug.Log(mode_tracker);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("isHit");
        playerHealth -= damage;
        shake.TriggerShake();
        if(playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}