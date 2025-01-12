using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public Camera camera;
    public Rigidbody cameraRigidbody;
    public float followSpeed = 0.3f;
    public float maxSpeed = 10f;
    public float zoomSpeed = 0.1f;

    public float minCameraOrthographicSize = 5f;
    public float maxCameraOrthographicSize = 10f;


    public Transform player;



    void FixedUpdate()
    {
        Vector3 targetPosition = player.position;
        
        cameraRigidbody.linearVelocity = Vector3.Lerp(cameraRigidbody.linearVelocity, (targetPosition - transform.position) * followSpeed, Time.fixedDeltaTime * maxSpeed);

        //calculate the size based on the speed of the camera rigibody
        float cameraOrthographicSize = Mathf.Lerp(minCameraOrthographicSize, maxCameraOrthographicSize, cameraRigidbody.linearVelocity.magnitude / maxSpeed);

        camera.orthographicSize = cameraOrthographicSize;
    }

}
