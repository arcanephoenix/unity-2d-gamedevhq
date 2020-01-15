using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 3;
    [SerializeField]
    private Transform explosionPrefab;
    [SerializeField]
    private AudioClip explosionClip;

    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            rotateSpeed = 0;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);
            Destroy(collision.gameObject);
            spawnManager.StartSpawn();
            Destroy(gameObject, 0.25f);
        }
    }
}
