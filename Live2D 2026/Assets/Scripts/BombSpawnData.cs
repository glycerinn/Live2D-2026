using UnityEngine;

[System.Serializable]
public class BombSpawnData
{
    public GameObject prefab;

    [Tooltip("Spawn every X seconds")]
    public float spawnRate;

    [Tooltip("Minute this bomb unlocks (0-4)")]
    public float unlockMinute;

    [HideInInspector]
    public float timer;
}