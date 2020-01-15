using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 3;
    [SerializeField]
    private Transform explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.25f);
        }
    }
}
