using UnityEngine;

public class RestrictedRandomMovement : MonoBehaviour
{
    public float maxRadius = 3f;
    public float moveDistance = 1f;
    public float updateInterval = 1f;

    private Vector2 currentPosition;
    private Vector2 spawnPoint;
    private Vector2 targetPosition;

    private void Start()
    {
        // Set the initial spawn point
        spawnPoint = transform.position;

        // Start the repeating movement
        InvokeRepeating("ChangeTargetPosition", 0f, updateInterval);
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }

    private void ChangeTargetPosition()
{
    // Calculate a random point within the maxRadius
    Vector2 randomDirection = Vector2.zero;

    // A random between 0 and 1, if we are below 0.5f we will go only on X axis, otherwise on Y axis
    if (Random.value < 0.5f)
    {
        // Another random to see if we will go + or - on the axis
        randomDirection.x = Random.value < 0.5f ? 1f : -1f;
    }
    else
    {
        // Same as above
        randomDirection.y = Random.value < 0.5f ? 1f : -1f;
    }
    // Now we check before moving if the new position will be outside the maxRadius
    //log the random direction
    Vector2 nextPosition = new Vector2(transform.position.x, transform.position.y) + randomDirection;
    //log the distance
    if (Vector2.Distance(spawnPoint, nextPosition) > maxRadius)
    {
        // If it is, we invert the direction
        //log if we are inverting the direction
        Debug.Log("Inverting the direction");
        randomDirection *= -1f;
        nextPosition = new Vector2(transform.position.x, transform.position.y) + randomDirection;
    }

    // Move towards the target position
    targetPosition = nextPosition;
}
}
