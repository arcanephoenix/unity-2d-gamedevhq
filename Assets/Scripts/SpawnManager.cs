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

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(SpawnPowerupCoroutine());
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(3);
        while(isAlive)
        {
            Transform newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), 9), Quaternion.identity);
            newEnemy.transform.parent = containerPrefab.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        yield return new WaitForSeconds(3);
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
