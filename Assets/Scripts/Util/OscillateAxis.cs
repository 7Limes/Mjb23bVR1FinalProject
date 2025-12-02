using UnityEngine;

public class OscillateAxis : MonoBehaviour
{
    private enum Axis { X, Y, Z }
    
    [Header("Oscillation Settings")]
    [Tooltip("The axis along which to oscillate")]
    [SerializeField] private Axis oscillationAxis = Axis.Y;
    
    [Tooltip("Distance to oscillate in units")]
    [SerializeField] private float distance = 2f;
    
    [Tooltip("Time for one complete cycle in seconds")]
    [SerializeField] private float period = 2f;
    
    [Tooltip("Time offset to control starting position (0-1 for one cycle)")]
    [SerializeField] private float timeOffset = 0f;
    
    private Vector3 startPos;
    private float time;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        if (period <= 0) return;
        
        time += Time.deltaTime;
        float cycles = (time + timeOffset) / period;
        float offset = Mathf.Sin(cycles * 2 * Mathf.PI) * distance;
        
        Vector3 newPos = startPos;
        
        switch (oscillationAxis)
        {
            case Axis.X:
                newPos.x += offset;
                break;
            case Axis.Y:
                newPos.y += offset;
                break;
            case Axis.Z:
                newPos.z += offset;
                break;
        }
        
        transform.position = newPos;
    }
}