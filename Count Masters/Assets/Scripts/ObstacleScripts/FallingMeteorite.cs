using UnityEngine;

public class FallingMeteorite : MonoBehaviour
{
    [Header("Fall Settings")]
    [SerializeField] private float fallSpeed = 5f;        // Vertical speed
    [SerializeField] private float sideSpeed = 2f;        // Horizontal speed
    [SerializeField] private float resetHeight = -5f;     // Y-position to reset
    [SerializeField] private float minDelay = 0f;         // Min delay before next fall
    [SerializeField] private float maxDelay = 1f;         // Max delay
    [SerializeField] private bool fallFromRight = true;   // Side boolean: true = right, false = left

    private Vector3 startPos;
    private bool isFalling = true;
    private float delayTimer = 0f;

    void Start()
    {
        startPos = transform.position;
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
            Vector3 move = Vector3.down * fallSpeed * Time.deltaTime;

            // Move sideways
            move += (fallFromRight ? Vector3.right : Vector3.left) * sideSpeed * Time.deltaTime;

            transform.Translate(move, Space.World);

            // Check if below threshold
            if (transform.position.y <= resetHeight)
            {
                isFalling = false;
                delayTimer = Random.Range(minDelay, maxDelay);

                // Reset position to original starting point
                transform.position = startPos;

                // Randomize side for next fall
                //fallFromRight = Random.value > 0.5f;

                isFalling = true;
            }
        }
    }
}
