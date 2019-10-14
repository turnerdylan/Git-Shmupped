using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        anim = GetComponent<Animator>();
        anim.SetTrigger("Collision");
    }

}
