using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Game")]
    public float gameDuration = 300f;

    [Header("Trash")]
    public GameObject trashPrefab;
    public float spawnRate = 10f;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    private float gameTimer;
    private float spawnTimer;

    void Start()
    {
        spawnTimer = Random.Range(0f, spawnRate);
    }

    void Update()
    {
        if (gameTimer >= gameDuration)
            return;

        gameTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnTrash();
        }
    }

    void SpawnTrash()
    {
       Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);
    }
}