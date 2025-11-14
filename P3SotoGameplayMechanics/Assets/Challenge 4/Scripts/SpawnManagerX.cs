using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15;
    private float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;

    public GameObject player;

    void Update()
    {
        // Count enemies properly
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // If all enemies are gone, spawn next wave
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // Spawn a powerup only if none exist
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerupPrefab,
                        GenerateSpawnPosition() + powerupSpawnOffset,
                        powerupPrefab.transform.rotation);
        }

        // Spawn enemies equal to waveCount
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab,
                        GenerateSpawnPosition(),
                        enemyPrefab.transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition();
    }

    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
