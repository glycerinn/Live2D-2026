using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [Header("Game")]
    public float gameDuration = 300f; // 5 minutes

    [Header("Bombs")]
    public BombSpawnData[] bombs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private float gameTimer;

    private readonly List<Bomb> activeBombs = new();

    void Start()
    {
        foreach (BombSpawnData bomb in bombs)
        {
            // Randomize the first spawn
            bomb.timer = Random.Range(0f, bomb.spawnRate);
        }
    }

    void Update()
    {
        if (gameTimer >= gameDuration)
            return;

        gameTimer += Time.deltaTime;
        activeBombs.RemoveAll(b => b == null);
        int maxBombs = Mathf.Clamp(Mathf.FloorToInt(gameTimer / 60f) + 1, 1, 5);

        foreach (BombSpawnData bomb in bombs)
        {
            // Has this bomb unlocked yet?
            if (gameTimer < bomb.unlockMinute)
                continue;

            bomb.timer += Time.deltaTime;

            // Wait until this bomb's own spawn timer finishes
            if (bomb.timer < bomb.spawnRate)
                continue;

            bomb.timer = 0f;

            // Don't exceed the max bombs on screen
            if (activeBombs.Count >= maxBombs)
                continue;

            SpawnBomb(bomb.prefab);
        }
    }

    void SpawnBomb(GameObject prefab)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject obj = Instantiate(prefab, point.position, Quaternion.identity);
        Bomb bomb = obj.GetComponent<Bomb>();

        if (bomb != null)
            activeBombs.Add(bomb);
    }

    public void BombRemoved(Bomb bomb)
    {
        activeBombs.Remove(bomb);
    }
}