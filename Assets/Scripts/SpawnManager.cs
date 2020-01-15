using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform enemyPrefab;
    [SerializeField]
    private Transform containerPrefab;
    [SerializeField]
    private Transform[] powerupPrefab;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(SpawnPowerupCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        while(isAlive)
        {
            Transform newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), 9), Quaternion.identity);
            newEnemy.transform.parent = containerPrefab.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        while (isAlive)
        {
            int randomPowerup = Random.Range(0, powerupPrefab.Length);
            Transform newEnemy = Instantiate(powerupPrefab[randomPowerup], new Vector3(Random.Range(-10, 10), 9), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,7f));
        }
    }

    public void Dead()
    {
        isAlive = false;
    }
}
