using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3Int targetPosition; // The grid position the player is moving towards
    private Rigidbody rigidbody;

    public float rubberBandForce = 10f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        targetPosition = Vector3Int.RoundToInt(transform.position); // Initialize to current grid position
    }

    void Update()
    {
        // Capture input and determine target position
        Vector3Int movement = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) // Move up
            movement = new Vector3Int(0, 1, 0);
        else if (Input.GetKeyDown(KeyCode.S)) // Move down
            movement = new Vector3Int(0, -1, 0);
        else if (Input.GetKeyDown(KeyCode.A)) // Move left
            movement = new Vector3Int(-1, 0, 0);
        else if (Input.GetKeyDown(KeyCode.D)) // Move right
            movement = new Vector3Int(1, 0, 0);

        if (movement != Vector3Int.zero)
        {
            targetPosition += movement;
        }
    }

    void FixedUpdate()
    {
        // Apply force directly based on the difference between target position and current position
        Vector3 forceDirection = (targetPosition - transform.position);
        rigidbody.AddForce(forceDirection * rubberBandForce, ForceMode.Force);
    }
}
