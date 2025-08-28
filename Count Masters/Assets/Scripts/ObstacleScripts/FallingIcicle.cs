using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    [Header("Fall Settings")]
    [SerializeField] private float fallSpeed = 5f;       // Speed of falling
    [SerializeField] private float resetHeight = -5f;    // Y-position to reset
    [SerializeField] private float minDelay = 0f;        // Min delay before falling again
    [SerializeField] private float maxDelay = 1f;        // Max delay

    private Vector3 startPos;
    private bool isFalling = true;
    private float delayTimer = 0f;

    void Start()
    {
        startPos = transform.position;
        // Optional: random delay so icicles start at different times
        delayTimer = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        if (isFalling)
        {
            // Move downward
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

            // Check if below threshold
            if (transform.position.y <= resetHeight)
            {
                isFalling = false;
                delayTimer = Random.Range(minDelay, maxDelay); // optional delay before next fall
                transform.position = startPos;                 // reset to original position
                isFalling = true;
            }
        }
    }
}