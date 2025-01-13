using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraHandler : MonoBehaviour
{
    public enum CameraMode
    {
        Follow,
        Cutscene
    }

    [Header("Camera Settings")]
    public Camera camera;
    public Rigidbody cameraRigidbody;
    public float followSpeed = 0.3f;
    public float maxSpeed = 10f;
    public float zoomSpeed = 0.1f;

    [Header("Orthographic Size Settings")]
    public float minCameraOrthographicSize = 5f;
    public float maxCameraOrthographicSize = 10f;
    public float orthoSpeedFactor = 0.1f;

    [Header("Follow Targets")]
    public CameraMode cameraMode = CameraMode.Follow;
    public Transform singleFollowTarget;
    public List<Transform> additionalTargets = new List<Transform>();

    [Header("Cutscene Settings")]
    public float panWeight = 0.5f; // Between 0 (fully favor single target) and 1 (equal weight for all targets)
    public float maxTargetDistance = 50f; // Maximum distance to include targets
    public float targetCheckInterval = 1f; // Interval in seconds to check target distances

    private float lastTargetCheckTime;

    void FixedUpdate()
    {
        if (singleFollowTarget == null)
            return;

        // Periodically check distances and remove far targets
        if (Time.time - lastTargetCheckTime >= targetCheckInterval)
        {
            RemoveDistantTargets();
            lastTargetCheckTime = Time.time;
        }

        if (cameraMode == CameraMode.Cutscene)
        {
            FollowCutscene();
        }
        else
        {
            Follow();
        }
    }

    private void FollowCutscene()
    {
        // Calculate weighted midpoint favoring the single target
        Vector3 weightedMidPoint = singleFollowTarget.position * (1 - panWeight);
        if (additionalTargets.Count > 0)
        {
            Vector3 multiTargetMidPoint = additionalTargets.Aggregate(Vector3.zero, (current, target) => current + target.position) / additionalTargets.Count;
            weightedMidPoint += multiTargetMidPoint * panWeight;
        }

        // Update camera position using Rigidbody velocity
        Vector3 targetPosition = weightedMidPoint;
        Vector3 velocity = (targetPosition - transform.position) * followSpeed;
        cameraRigidbody.linearVelocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Adjust orthographic size based on the distance between valid targets
        float maxDistance = GetMaxDistanceBetweenTargets(additionalTargets);
        float targetSize = Mathf.Lerp(minCameraOrthographicSize, maxCameraOrthographicSize, maxDistance / maxCameraOrthographicSize);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, Time.fixedDeltaTime * zoomSpeed);
    }

    private void Follow()
    {
        // Follow the single target directly
        Vector3 targetPosition = singleFollowTarget.position;
        Vector3 velocity = (targetPosition - transform.position) * followSpeed;
        cameraRigidbody.linearVelocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //size based on velocity


        float targetOrthoSize = Mathf.Lerp(minCameraOrthographicSize, maxCameraOrthographicSize, cameraRigidbody.linearVelocity.magnitude / maxSpeed);

        // Adjust orthographic size to minimum size for a single target
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetOrthoSize, Time.fixedDeltaTime * zoomSpeed);
    }

    private void RemoveDistantTargets()
    {
        additionalTargets.RemoveAll(target => Vector3.Distance(singleFollowTarget.position, target.position) > maxTargetDistance);
    }

    private float GetMaxDistanceBetweenTargets(List<Transform> targets)
    {
        float maxDistance = 0f;

        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = i + 1; j < targets.Count; j++)
            {
                float distance = Vector3.Distance(targets[i].position, targets[j].position);
                maxDistance = Mathf.Max(maxDistance, distance);
            }
        }

        return maxDistance;
    }

}
