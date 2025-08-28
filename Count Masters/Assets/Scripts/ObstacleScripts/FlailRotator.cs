using UnityEngine;

public class FlailRotator : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float maxAngle = 30f;   // Max swing angle (±30)
    [SerializeField] private float swingSpeed = 2f;  // Speed of swinging
    [SerializeField] private bool randomize = true;

    private float randomOffset;
    private float randomSpeed;

    void Start()
    {
        // Randomize starting phase & speed if enabled
        if (randomize)
        {
            randomOffset = Random.Range(0f, Mathf.PI * 2f); // random start point in sine wave
            randomSpeed = Random.Range(swingSpeed * 0.8f, swingSpeed * 1.2f); // small speed variation
        }
        else
        {
            randomOffset = 0f;
            randomSpeed = swingSpeed;
        }
    }

    void Update()
    {
        // Use sine wave to swing smoothly back & forth between -maxAngle and +maxAngle
        float angle = Mathf.Sin(Time.time * randomSpeed + randomOffset) * maxAngle;

        // Apply rotation around Z (so flail swings left-right, pivot stays fixed)
        transform.localRotation = Quaternion.Euler(0f, 90f, angle);
    }
}
