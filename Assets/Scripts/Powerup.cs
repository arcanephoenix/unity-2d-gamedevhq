using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if(player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TurnOnTripleshot();
                        break;
                    case 1:
                        player.TurnOnSpeed();
                        break;
                    case 2:
                        player.TurnOnShield();
                        break;
                    default:
                        Debug.LogError("what kind of powerup is this wtf");
                        break;
                }
            }

            Destroy(gameObject);
        }
    }
}
