using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine(SpawnPowerupRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn gameObjects every five seconds
    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            float RandomY = 7f;
            float RandomX = Random.Range(-8.5f, 8.5f);
            GameObject newEnemy =  Instantiate(_enemy, new Vector3(RandomX,RandomY),Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (stopSpawning == false)
        {
            float RandY = 7f;

            float RandX = Random.Range(-8.5f, 8.5f);
            int randomPowerUp = Random.Range(0, 2);

            Instantiate(powerups[randomPowerUp], new Vector3(RandX, RandY), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    public void playerIsDead()
    {
        stopSpawning = true;
    }
}
