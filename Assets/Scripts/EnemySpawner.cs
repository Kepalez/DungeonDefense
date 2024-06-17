using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnEnemy(){
        GameObject spawnedEnemy = Instantiate(Enemies[Random.Range(0,GameManager.Instance.enemyVariety)],transform.position,Quaternion.identity);
        return spawnedEnemy;
    }
}
