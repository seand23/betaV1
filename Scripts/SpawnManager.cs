using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    //displaying wave
    public TextMeshProUGUI waveCountText;
    //enemy
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    private float spawnRange = 20;
    public int bossRound;
    public int enemyCount;
    public static int waveCount = 1;
    //powerup
    public GameObject[] powerupPrefabs;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(enemyCount);    
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0)
            {
                waveCount++;
                waveCountText.text = "Wave:" + waveCount;
                if (waveCount % bossRound == 0)
                {
                    SpawnBossWave(waveCount);
                }
                else
                {
                    SpawnEnemyWave(waveCount);
                }

                int randomPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosx, 1, spawnPosz);

        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (gameManager.isGameActive)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int randomEnemy = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
            }
        }
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if (gameManager.isGameActive)
        {
            if (bossRound != 0)
            {
                miniEnemysToSpawn = currentRound / bossRound;
                SpawnMiniEnemy(4);
            }
            var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        }
    }

    public void SpawnMiniEnemy(int amount)
    {
        if (gameManager.isGameActive)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
                Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
            }
        }
    }
}
