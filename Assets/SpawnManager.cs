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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn gameObjects every five seconds
    IEnumerator SpawnRoutine()
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

    public void playerIsDead()
    {
        stopSpawning = true;
    }
}