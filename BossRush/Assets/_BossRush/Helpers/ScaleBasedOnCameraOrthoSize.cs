using UnityEngine;

public class ScaleBasedOnCameraOrthoSize : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float minOrthoRange;
    [SerializeField] float maxOrthoRange;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float orthoSize = camera.orthographicSize;
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minOrthoRange, maxOrthoRange, orthoSize));
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
