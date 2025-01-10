using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float movementForce = 10f; // Force applied per key press
    public float rubberBandForce = 10f; // Force pulling to the nearest grid point
    public float cellSize = 1f; // Grid cell size

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply movement forces based on input
        if (Input.GetKey(KeyCode.W)) // Move up
            rigidbody.AddForce(Vector3.up * movementForce, ForceMode.Force);
        if (Input.GetKey(KeyCode.S)) // Move down
            rigidbody.AddForce(Vector3.down * movementForce, ForceMode.Force);
        if (Input.GetKey(KeyCode.A)) // Move left
            rigidbody.AddForce(Vector3.left * movementForce, ForceMode.Force);
        if (Input.GetKey(KeyCode.D)) // Move right
            rigidbody.AddForce(Vector3.right * movementForce, ForceMode.Force);

        // Calculate nearest grid point
        Vector3 currentPosition = transform.position;
        Vector3 nearestGridPoint = new Vector3(
            Mathf.Round(currentPosition.x / cellSize) * cellSize,
            Mathf.Round(currentPosition.y / cellSize) * cellSize,
            Mathf.Round(currentPosition.z / cellSize) * cellSize
        );

        // Apply rubber band force to pull towards nearest grid point
        Vector3 forceDirection = nearestGridPoint - currentPosition;
        rigidbody.AddForce(forceDirection * rubberBandForce, ForceMode.Force);

    }

    public void Push(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Force);
    }
}
