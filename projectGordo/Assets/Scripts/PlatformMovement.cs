using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float initialSpeed = 5f;  // Starting speed of the platform
    public float speedIncreaseRate = 0.1f;  // Rate at which speed increases over time
    public float resetPositionX = -10f;  // Position where the platform resets
    public float platformWidth = 10f;  // Width of the platform for resetting

    private float currentSpeed;  // Current speed of the platform
    private Vector2 moveDirection;  // Reusable vector for movement direction
    private float timeSinceLastSpeedIncrease;  // Timer for speed increase
    private float speedIncreaseInterval = 1f;  // Interval in seconds to increase speed

    void Start()
    {
        currentSpeed = initialSpeed;  // Initialize the current speed
        moveDirection = Vector2.left * currentSpeed;  // Set the movement direction to the left (2D)
    }

    void Update()
    {
        MovePlatform();
        IncreaseDifficulty();
    }

    void MovePlatform()
    {
        // Move the platform to the left by the current speed
        transform.Translate(moveDirection * Time.deltaTime);

        // Reset the platform once it moves off-screen
        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector2(resetPositionX + platformWidth, transform.position.y);  // Reset position based on width
        }
    }

    void IncreaseDifficulty()
    {
        // Increase speed after the specified interval
        timeSinceLastSpeedIncrease += Time.deltaTime;
        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
            currentSpeed += speedIncreaseRate;  // Increase speed
            moveDirection = Vector2.left * currentSpeed;  // Update movement direction
            timeSinceLastSpeedIncrease = 0f;  // Reset the timer
        }
    }
}
