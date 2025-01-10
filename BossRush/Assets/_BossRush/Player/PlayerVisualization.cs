using UnityEngine;

public class PlayerVisualization : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Transform followVelocity;
    public Transform followShootDirection;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.linearVelocity.magnitude > 0.1f)
        {
            followVelocity.forward = rigidbody.linearVelocity.normalized;
        }

        // Aim followShootDirection to the mouse

        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert mouse position to world space at the same depth as the player
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            mouseScreenPosition.x, 
            mouseScreenPosition.y, 
            Camera.main.WorldToScreenPoint(transform.position).z));

        // Calculate the direction from the player to the mouse
        Vector3 direction = (mouseWorldPosition - followShootDirection.position).normalized;

        // Set the forward direction of followShootDirection to look at the mouse
        followShootDirection.forward = direction;
    }
}
