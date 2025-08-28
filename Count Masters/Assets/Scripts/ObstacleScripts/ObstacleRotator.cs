using UnityEngine;

public class GroundRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second
    [SerializeField] private bool randomizeStartRotation = true;
    [SerializeField] private bool randomizeDirection = true;

    void Start()
    {
        if (randomizeStartRotation)
        {
            // Randomize the starting Y rotation only (so it stays parallel to ground)
            Vector3 euler = transform.eulerAngles;
            euler.y = Random.Range(0f, 360f);
            transform.eulerAngles = euler;
        }

        if (randomizeDirection)
        {
            // 50/50 chance to flip direction
            if (Random.value > 0.5f)
                rotationSpeed *= -1f;
        }
    }

    void Update()
    {
        // Rotate only around Y axis (parallel to XZ plane / ground)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
