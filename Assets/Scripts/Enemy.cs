using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private GameObject laserPrefab;
    private Animator animator;
    private Player playa;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("no audiosource");
        playa = GameObject.Find("Player").GetComponent<Player>();
        if (playa == null) Debug.LogError("NO PLAYER FOUND REEE");
        animator = gameObject.GetComponent<Animator>();
        if (animator == null) Debug.LogError("NO ANIMATOR REEE");
        StartCoroutine(EnemyShooting());
    }

    IEnumerator EnemyShooting()
    {
        while(true)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(-0.24f,-1.68f), Quaternion.identity);
            Instantiate(laserPrefab, transform.position + new Vector3(0.24f, -1.68f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            // Instantiate(gameObject, new Vector3(Random.Range(-10, 10), 9), Quaternion.identity);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (playa != null) playa.Damage();
            speed = 0;
            //rigidbody2D.dete
            animator.SetTrigger("OnEnemyDead");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,2.5f);
        }
        else if(other.tag == "Laser")
        {
            if (playa != null) playa.UpdateScore(10);
            Destroy(other.gameObject);
            
            speed = 0;
            animator.SetTrigger("OnEnemyDead");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,2.5f);
        }
    }
}
