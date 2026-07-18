using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [Header("Game")]
    public float gameDuration = 300f; // 5 minutes

    [Header("Bombs")]
    public BombSpawnData[] bombs;

    [Header("Spawn Points")]
    public Transform spawnPoint;

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

        // Max bombs on screen (1 -> 5)
        int maxBombs = Mathf.Clamp(Mathf.FloorToInt(gameTimer / 60f) + 1, 1, 5);

        // Update timers for unlocked bombs
        foreach (BombSpawnData bomb in bombs)
        {
            if (gameTimer >= bomb.unlockMinute)
            {
                bomb.timer += Time.deltaTime;
            }
        }

        // Screen is full
        if (activeBombs.Count >= maxBombs)
            return;

        // Find every bomb that's ready to spawn
        List<BombSpawnData> readyBombs = new();

        foreach (BombSpawnData bomb in bombs)
        {
            if (gameTimer < bomb.unlockMinute)
                continue;

            if (bomb.timer >= bomb.spawnRate)
                readyBombs.Add(bomb);
        }

        // Nothing ready yet
        if (readyBombs.Count == 0)
            return;

        // Pick one randomly
        BombSpawnData chosen = readyBombs[Random.Range(0, readyBombs.Count)];
        chosen.timer = 0f;

        SpawnBomb(chosen.prefab);
    }

    void SpawnBomb(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Bomb bomb = obj.GetComponent<Bomb>();

        if (bomb != null)
            activeBombs.Add(bomb);
    }

    public void BombRemoved(Bomb bomb)
    {
        activeBombs.Remove(bomb);
    }
}