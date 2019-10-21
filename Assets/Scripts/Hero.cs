using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    Animator anim;
    ShieldScript shield;
    ScreenShake shake;

    public AudioClip deathSFX;


    [Header("Player specs")]
    public float speed = 30;
    public float slowSpeed = 15;
    public float playerHealth;
    public int playerScore = 0;
    [SerializeField] [Range(0, 1)] float volume = 0.7f;

    //public float gameRestartDelay = 2f;

    [Header("UI mode controls")]
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI scoreText;
    string[] modes = { "gun", "shield", "laser", "bomb" };
    public int mode_tracker = 0;

    private void Start()
    {
        shield = FindObjectOfType<ShieldScript>();
        anim = GetComponent<Animator>();
        shake = FindObjectOfType<ScreenShake>();
        tmp.text = modes[mode_tracker];
        hp.text = playerHealth.ToString("F0");
    }

    void Update()
    {
        //get input and move the player around
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        //Debug.Log(speed);
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
            
            if (mode_tracker == 3)
            {
                mode_tracker = 0;
            }
            else
            {
                mode_tracker++;
            }
            tmp.text = modes[mode_tracker];
            //Debug.Log(mode_tracker);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        Destroy(coll.gameObject);
        if (coll.gameObject.GetComponent<DamageDealer>())
        {
            //deal damage to enemy
            int damage = coll.gameObject.GetComponent<DamageDealer>().damageValue;
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("isHit");
        playerHealth -= damage;
        hp.text = playerHealth.ToString("F0");
        shake.TriggerShake();
        if(playerHealth <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volume);
            SceneManager.LoadScene("Game Over");
        }
    }

    public void AddHealth()
    {
        playerHealth += 20;
        if(playerHealth > 100)
        {
            playerHealth = 100;
        }
        hp.text = playerHealth.ToString("F0");
    }

    public void SlowSpeed()
    {
        speed = slowSpeed;
    }

    public void FastSpeed()
    {
        speed = 30;
    }

    public void AddScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
    }
}