using UnityEngine;

public class RestrictedRandomMovement : MonoBehaviour
{
    public float maxRadius = 3f;
    public float moveDistance = 1f;
    public float updateInterval = 1f;
     public float speed = 0f; // Added speed field
    public Animator animator; // Added Animator field
    private Vector2 currentPosition;
    private Vector2 spawnPoint;
    private Vector2 targetPosition;

    private void Start()
    {
        // Set the initial spawn point
        spawnPoint = transform.position;

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Start the repeating movement
        InvokeRepeating("ChangeTargetPosition", 0f, updateInterval);
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        // Update animator parameters based on movement
        UpdateAnimatorParameters();
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

private void UpdateAnimatorParameters()
    {
        // Determine the direction of movement
        float horizontal = targetPosition.x - transform.position.x;
        float vertical = targetPosition.y - transform.position.y;
        //check if the current position is different from the target position, if so we are moving
        // if it's not we set the speed to 0
        if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            speed = 1;
        } else {
            speed = 0;
        }
        // Update Animator parameters
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("LastMoveHorizontal", horizontal);
        animator.SetFloat("LastMoveVertical", vertical);
        animator.SetFloat("Speed", speed); // Added speed parameter
    }
}
