using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private ScreenShake shake;
    Rigidbody rigidB;
    public AudioClip bombSFX;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        anim = GetComponent<Animator>();
        shake = FindObjectOfType<ScreenShake>();
        rigidB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigidB.velocity += new Vector3(0, .75f, 0);
    }

    private void OnTriggerStay(Collider coll)
    {
        anim.SetBool("Exploded", true);
        AudioSource.PlayClipAtPoint(bombSFX, Camera.main.transform.position, 0.5f);
        shake.TriggerShake();
        rigidB.velocity = Vector3.zero;

        StartCoroutine(DestroyBomb());

    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(.33f);
        Destroy(gameObject);
    }

}
