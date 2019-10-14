using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ShieldScript : MonoBehaviour
{
    public GameObject shield;
    Hero hero;
    public TextMeshProUGUI text;
    public float maxShieldEnergy;
    private float currentShieldEnergy;
    public float depleteRate;
    public float increaseRate;
    public float slowSpeed = 15;

    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        currentShieldEnergy = maxShieldEnergy;
    }

    private void Update()
    {
        if(currentShieldEnergy < maxShieldEnergy)
        {
            currentShieldEnergy += Time.deltaTime * increaseRate;
        }
        if (Input.GetKey(KeyCode.Space) && currentShieldEnergy > 0 && hero.mode_tracker == 1)
        {
            currentShieldEnergy -= Time.deltaTime * depleteRate;
            shield.transform.position = hero.transform.position;
            hero.speed = slowSpeed;
        }
        else
        {
            shield.transform.position = new Vector3(150, 0, 0);
            hero.speed = 30;
        }

        text.text = currentShieldEnergy.ToString("F0");
    }
}