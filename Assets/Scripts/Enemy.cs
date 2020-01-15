using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    private Animator animator;
    private Player playa;
    private AudioSource audioSource;
    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("no audiosource");
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null) Debug.LogError("no rigidbody");
        playa = GameObject.Find("Player").GetComponent<Player>();
        if (playa == null) Debug.LogError("NO PLAYER FOUND REEE");
        animator = gameObject.GetComponent<Animator>();
        if (animator == null) Debug.LogError("NO ANIMATOR REEE");
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
            Destroy(gameObject,2.5f);
        }
        else if(other.tag == "Laser")
        {
            if (playa != null) playa.UpdateScore(10);
            Destroy(other.gameObject);
            //Destroy(rigidbody2D);
            speed = 0;
            animator.SetTrigger("OnEnemyDead");
            audioSource.Play();
            Destroy(gameObject,2.5f);
        }
    }
}
