using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private Transform laserPrefab;
    [SerializeField]
    private float laserOffset = 0.8f;
    [SerializeField]
    private float fireRate = 0.5f;
    private float nextFire = -1;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private Transform tripleShotPrefab;
    [SerializeField]
    private float powerupTime = 5f;    
    [SerializeField]
    private float boostSpeed = 8.5f;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private AudioClip playerExplosion;
    [SerializeField]
    private AudioClip laserShot;

    private AudioSource audioSource;
    private int score;
    private SpawnManager spawnManager;
    private bool isTripleShot = false;
    private bool isSpeed = false;
    private bool isShield = false;
    private UIManager uiManager;
    private GameObject rightWing;
    private GameObject leftWing;
    
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("no audiosource");
        score = 0;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rightWing = GameObject.Find("RightFire");
        leftWing = GameObject.Find("LeftFire");
        rightWing.SetActive(false);
        leftWing.SetActive(false);
        uiManager = canvas.GetComponent<UIManager>();
        shield.SetActive(isShield);
        if (spawnManager == null) Debug.LogError("SpawnManager is null");
        if (uiManager == null) Debug.LogError("UIManager is null");
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        // firing the laser
        if (Input.GetAxis("Fire1") == 1 && Time.time > nextFire) FireLaser();
        PlayerMovement();
        
    }

    void PlayerMovement()
    {
        float hozInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(hozInput, verInput);
        if (isSpeed)
        {
            transform.Translate(direction * boostSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }


        if (transform.position.y >= 0.0f) transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        else if (transform.position.y < -3.8f) transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);

        if (transform.position.x > 11.3f) transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        else if (transform.position.x < -11.3f) transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
    }

    void FireLaser()
    {
       nextFire = Time.time + fireRate;
       if (isTripleShot)
       {
           Instantiate(tripleShotPrefab, transform.position + new Vector3(0f, laserOffset), Quaternion.identity);
       }
       else
       {
           Instantiate(laserPrefab, transform.position, Quaternion.identity);
       }
        audioSource.clip = laserShot;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyLaser")
        {
            Destroy(collision.gameObject);
            Damage();
        }
    }

    public void Damage()
    {
        if(isShield)
        {
            isShield = false;
            shield.SetActive(isShield);
            return;
        }
        lives--;
        if (lives == 2) rightWing.SetActive(true);
        else if (lives == 1) leftWing.SetActive(true);
        uiManager.SetLivesSprite(lives);
        if (lives < 1)
        {
            audioSource.clip = playerExplosion;
            audioSource.Play();
            spawnManager.Dead();
            uiManager.SetGameOver();
            Destroy(this.gameObject);
        }
    }

    public void TurnOnTripleshot()
    {
        isTripleShot = true;
        StartCoroutine(TripleShotCountdown());
    }

    public void TurnOnShield()
    {
        isShield = true;
        shield.SetActive(isShield);
    }

    public void TurnOnSpeed()
    {
        isSpeed = true;
        StartCoroutine(SpeedCountdown());
    }

    IEnumerator TripleShotCountdown()
    {
        yield return new WaitForSeconds(powerupTime);
        isTripleShot = false;
    }

    IEnumerator SpeedCountdown()
    {
        yield return new WaitForSeconds(powerupTime);
        isSpeed = false;
    }

    public void UpdateScore(int points)
    {
        score += points;
        canvas.GetComponent<UIManager>().SetPlayerScore(score);
    }
}
