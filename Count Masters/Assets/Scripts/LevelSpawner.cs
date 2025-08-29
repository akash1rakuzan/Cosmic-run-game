using UnityEngine;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private GameObject[] blockPrefabs;   // Different block prefabs (mountain, floor, obstacle)
    [SerializeField] private int initialBlocks = 5;       // How many blocks to spawn at start
    [SerializeField] private float blockLength = 20f;   // Length of one block
    [SerializeField] private Transform player;            // Reference to player transform
    [SerializeField] private int maxBlocks = 7;           // Max blocks in scene at once

    private Queue<GameObject> spawnedBlocks = new Queue<GameObject>();
    private float spawnZ = 0f;

    void Start()
    {
        // Spawn initial blocks
        for (int i = 0; i < initialBlocks; i++)
        {
            SpawnBlock();
        }
    }

    void Update()
    {
        // Check if we need to spawn a new block
        if (player.position.z - blockLength > spawnZ - (maxBlocks * blockLength))
        {
            SpawnBlock();
            DeleteOldestBlock();
        }
    }

    private void SpawnBlock()
    {
        // Pick a random block prefab
        GameObject prefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

        // Instantiate at the correct Z offset
        Vector3 spawnPos = new Vector3(0, 0, spawnZ);
        GameObject newBlock = Instantiate(prefab, spawnPos, Quaternion.identity);

        spawnedBlocks.Enqueue(newBlock);

        spawnZ += blockLength; // Update Z for next block
    }

    private void DeleteOldestBlock()
    {
        if (spawnedBlocks.Count > maxBlocks)
        {
            GameObject oldBlock = spawnedBlocks.Dequeue();
            Destroy(oldBlock);
        }
    }
}
