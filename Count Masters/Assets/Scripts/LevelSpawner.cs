using UnityEngine;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private GameObject[] blockPrefabs;   // Different block prefabs (mountain, floor, obstacle)
    [SerializeField] private int initialBlocks = 5;       // How many blocks to spawn at start
    [SerializeField] private float blockLength = 20f;     // Length of one block
    [SerializeField] private Transform player;            // Reference to player transform
    [SerializeField] private int maxBlocks = 7;           // Max blocks in scene at once

    private Queue<GameObject> spawnedBlocks = new Queue<GameObject>();
    private float spawnZ = 0f;
    private int blocksSpawned = 0; // Count how many blocks we've spawned
    private int numberOfBlocksInLevel; // Randomly chosen total number of blocks in this level

    void Start()
    {
        // Randomize the total number of blocks for this level (10–15)
        numberOfBlocksInLevel = Random.Range(10, 16);
       

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

        // Stop spawning if we've reached the limit
        if (blocksSpawned >= numberOfBlocksInLevel)
            return;

        GameObject prefab;

        // If this is the *last* block in the level -> FinishBlock (index 11)
        if (blocksSpawned == numberOfBlocksInLevel - 1)
        {
            prefab = blockPrefabs[11];
        }

        else if (blocksSpawned == 0)
        {
            // First block: always StartBlock (index 0)
            prefab = blockPrefabs[0];
        }
        else if (blocksSpawned == 1)
        {
            //  Gates block 
            prefab = blockPrefabs[9];
        }
        else if (blocksSpawned % 4 == 0)
        {
            // Every 4th block: always Gates block (index 9)
            prefab = blockPrefabs[9];
        }
        else
        {
            // Otherwise: pick random from index 1 and up
            prefab = blockPrefabs[Random.Range(1, blockPrefabs.Length - 1)];
        }

        Vector3 spawnPos = new Vector3(0, 0, spawnZ);
        GameObject newBlock = Instantiate(prefab, spawnPos, Quaternion.identity);

        spawnedBlocks.Enqueue(newBlock);
        spawnZ += blockLength;
        blocksSpawned++;
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
