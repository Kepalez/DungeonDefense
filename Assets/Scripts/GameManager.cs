using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake(){
        if(Instance == null){Instance = this;}
    }

    #endregion
    public List <GameObject> enemySpawners = new List<GameObject>();
    public List<float> timeMilestones;
    public bool singleEnemySpawn;
    public Transform enemyParent;
    public Transform playerSpawnersParent;
    public float spawnInterval = 2f;
    public float realSpawnInterval;
    private float nextSpawnTime = 0f;
    public int enemyVariety;
    public int enemiesCounter;

    public int playerMoney;
    public int playerTotalLives;
    public int playerLives;
    
    public float survivedTime;
    public int currentPhase;

    public CountTxt moneyAmount;

    public changeHeart heartsCount;

    public ProgressBar enemyWaves;

    private float winDelay = 2f;
    private float winDelayCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerLives = playerTotalLives;
        timeMilestones = new List<float>(){
        55,90,180,225,300,370,400
        };
        singleEnemySpawn = true;
        enemiesCounter = 1;
        nextSpawnTime = 0f;
        currentPhase = -1;
        realSpawnInterval = 10f;
        foreach(Transform child in enemyParent) Destroy(child.gameObject);
        enemyWaves.setMax((int)timeMilestones[timeMilestones.Count - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        survivedTime += Time.deltaTime;
        if(singleEnemySpawn){
            nextSpawnTime+= Time.deltaTime;
            if(nextSpawnTime > realSpawnInterval){
                nextSpawnTime = 0f;
                realSpawnInterval = Random.Range(spawnInterval*0.8f,spawnInterval);
                spawnRandom();
            }
        }
        moneyAmount.UpdateTxt(playerMoney,"");
        heartsCount.setHeart(playerLives-1);
        enemyWaves.setProgress(survivedTime);
        levelSchedule();
    }

    private void levelSchedule()
    {
        if(currentPhase == timeMilestones.Count && enemyParent.childCount == 0){
            //El jugador gana
            //Cambiamos de escena?
            Debug.Log("Player win!!!");
            winDelayCounter+=Time.deltaTime;
            if(winDelay <=winDelayCounter){
                SceneManager.LoadScene("GameWon");
            }
            return;
        }
        int newPhase = 0;
        for(int i = 0;i < timeMilestones.Count;i++){
            if(survivedTime > timeMilestones[i]) newPhase++;
        }
        if(newPhase > currentPhase){
            currentPhase = newPhase;
            changePhase();
        }
        if(playerLives<=0){
            SceneManager.LoadScene("GameOver");
        }
    }

    private void changePhase()
    {
        switch(currentPhase){
            case 0:
                singleEnemySpawn = true;
                spawnInterval = 10f;
                enemyVariety = 1;
                break;
            case 1:
                spawnInterval = 8f;
                enemyVariety = 2;
                break;
            case 2:
                StartCoroutine("throwWave");
                break;
            case 3:
                enemyVariety = 3;
                break;
            case 4:
                spawnInterval = 2f;
                break;
            case 5:
                StartCoroutine("throwWave");
                enemiesCounter++;
                break;
            case 6:
                singleEnemySpawn = false;
                StartCoroutine("throwWave");
                enemiesCounter++;
                break;
            case 7:
                //Solo es para que la victoria quede habilitada
                break;
        }   
    }

    IEnumerator throwWave()
    {
        if(currentPhase == 3){
            int enemiesAmount = Random.Range(10,15);
            for(int i = 0; i < enemiesAmount;i++){
                spawnRandom();
                yield return new WaitForSeconds(0.25f);
            }
        }
        if(currentPhase == 5){
            int enemiesAmount = Random.Range(30,40);
            for(int i = 0; i < enemiesAmount;i++){
                spawnRandom();
                yield return new WaitForSeconds(0.25f);
            }
        }
        if(currentPhase == 6){
            int enemiesAmount = Random.Range(50,60);
            for(int i = 0; i < enemiesAmount;i++){
                spawnRandom();
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

    public void enableSpawnerStatusCheck(){
        foreach(Transform child in playerSpawnersParent){
            child.position = new Vector3(child.position.x,child.position.y,-1);
            DropUnit dp = child.gameObject.GetComponent<DropUnit>();
            dp.showStatus();
        }
    }

    public void disableSpawnerStatusCheck(){
        foreach(Transform child in playerSpawnersParent){
            child.position = new Vector3(child.position.x,child.position.y,0);
            DropUnit dp = child.gameObject.GetComponent<DropUnit>();
            dp.hideStatus();
        }
    }

    public void spawnRandom(){
        GameObject spawnedEnemy = enemySpawners[Random.Range(0,enemySpawners.Count)].GetComponent<EnemySpawner>().SpawnEnemy();
        spawnedEnemy.transform.parent = enemyParent;
    }

    public void playerLoseLive(){
        playerLives--;
        Debug.Log("Player lost a live! "+playerLives.ToString()+" remaining");
    }
}
