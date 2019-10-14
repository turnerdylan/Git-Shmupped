using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private ScreenShake shake;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        anim = GetComponent<Animator>();
        shake = FindObjectOfType<ScreenShake>();
    }

    private void OnTriggerStay(Collider coll)
    {
        anim.SetBool("Exploded", true);
        shake.TriggerShake();
        //Rigidbody rigidB = GetComponent<Rigidbody>();
        //rigidB.velocity = Vector3.zero;
        StartCoroutine(DestroyBomb());

    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(.33f);
        Destroy(gameObject);
    }

}
