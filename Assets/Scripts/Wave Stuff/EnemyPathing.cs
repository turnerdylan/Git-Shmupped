using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    Waves waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    public bool destroyOnEnd = false;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(Waves waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            if (destroyOnEnd)
            {
                Destroy(gameObject);
            }
            Debug.Log("reached the end");
        }
    }
}
