using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [Header("Game")]
    public float gameDuration = 300f; // 5 minutes

    [Header("Bombs")]
    public BombSpawnData[] bombs;

    [Header("Spawn Area")]
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

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

        // Count only bombs that haven't been defused yet
        int activeUndefusedBombs = 0;

        foreach (Bomb activeBomb in activeBombs)
        {
            if (activeBomb != null && !activeBomb.IsDefused)
            {
                activeUndefusedBombs++;
            }
        }

        // Screen is full of active (undefused) bombs
        if (activeUndefusedBombs >= maxBombs)
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
        Vector2 randomPos = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
        );

        GameObject obj = Instantiate(prefab, randomPos, Quaternion.identity);

        Bomb bomb = obj.GetComponent<Bomb>();

        if (bomb != null)
            activeBombs.Add(bomb);
    }

    public void BombRemoved(Bomb bomb)
    {
        activeBombs.Remove(bomb);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 center = (minSpawnPosition + maxSpawnPosition) / 2f;
        Vector3 size = maxSpawnPosition - minSpawnPosition;

        Gizmos.DrawWireCube(center, size);
    }
}