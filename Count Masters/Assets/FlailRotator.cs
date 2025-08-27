using UnityEngine;

public class FlailRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // Rotation speed in degrees per second
    public Vector3 rotationSpeed = new Vector3(0, 90, 0); // Y-axis, 90°/s

    void Update()
    {
        // Smoothly rotate based on time and speed
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

