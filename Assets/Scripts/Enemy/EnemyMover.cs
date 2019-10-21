using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    //double helix
    //x formation
    //move/shoot towards the player
    //come from the bottom

    Hero hero;
    public enum Type { sinWave, points, atPlayer, straight};
    public Type currentType;

    public float speed;

    //points
    int waypointIndex = 0;
    public Transform [] waypoints;


    //sin wave
    Vector3 newPos;
    Vector3 _startPosition;
    public float sinWidth = 7;

    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        _startPosition = transform.position;
        newPos = new Vector3(0, 0, 0).normalized;
        if (currentType == Type.points)
        {
            points();
        }
    }

    // Start is called before the first frame update
    void Update()
    {

        if(currentType == Type.sinWave)
        {
            moveSin();
        } else if (currentType == Type.atPlayer)
        {
            atPlayer();
        } else if (currentType == Type.straight)
        {
            straight();
        }
    }

    private void moveSin()
    {
        newPos.y -= speed * Time.deltaTime;
        //Debug.Log(newPos.y);
        transform.position = _startPosition + new Vector3((sinWidth * Mathf.Sin(Time.time)), newPos.y, 0f);

    }

    private void points()
    {
        //Debug.Log("current index is " + waypointIndex);
        if (waypointIndex <= waypoints.Length - 1)
        {
            //Debug.Log("moving");
            Vector3 targetPosition = waypoints[waypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

            //Debug.Log("target is " + targetPosition);
            //Debug.Log("current is " + transform.position);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
    }

    private void atPlayer()
    {
        if (hero)
        {
            Vector3 heroPos = hero.transform.position;
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 moveDir = (heroPos - transform.position).normalized * speed;
            rb.AddForce(moveDir);
        }
    }

    private void straight()
    {
        Vector3 tempPos = transform.position;
        tempPos.y -= speed * Time.deltaTime; transform.position = tempPos;
    }

}
