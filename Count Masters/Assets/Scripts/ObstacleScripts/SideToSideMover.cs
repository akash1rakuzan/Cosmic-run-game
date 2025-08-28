using UnityEngine;

public class SideToSideMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;     // Movement speed
    [SerializeField] private float minX = -6f;     // Left limit
    [SerializeField] private float maxX = 6f;      // Right limit

    private float startY;
    private float startZ;
    private float randomOffset;  // Each obstacle gets a unique offset

    void Start()
    {
        // Save Y/Z so we only move on X
        startY = transform.position.y;
        startZ = transform.position.z;

        // Random phase offset so copies don't sync
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float range = maxX - minX;

        // Add the random offset into PingPong calculation
        float x = Mathf.PingPong((Time.time + randomOffset) * speed, range) + minX;

        transform.position = new Vector3(x, startY, startZ);
    }
}
