using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor.PackageManager.UI;
using System.Linq;

public class FlickDetector : MonoBehaviour {
    [SerializeField] private float highThreshold = 250.0f;
    [SerializeField] private float lowThreshold = 30.0f;
    [SerializeField] private int sampleCount = 10;
    [SerializeField] private float samplePercentage = 0.5f;
    [SerializeField] private float cooldown = 0.25f;

    [SerializeField] private UnityEvent flickEvent;
    private Queue<Vector3> angularVelocityBuffer = new Queue<Vector3>();
    private Quaternion previousRotation = Quaternion.Euler(0, 0, 0);
    private Vector3 highAngularVelocity = Vector3.zero;
    private Vector3 lowAngularVelocity = Vector3.zero;

    private float cooldownTimer = 0.0f;

    Vector3 AverageVelocities(int index, int amount) {
        Vector3 sum = Vector3.zero;
        for (int i = index; i < amount; i++) {
            sum += angularVelocityBuffer.ElementAt(i);
        }
        return sum / amount;
    }

    void UpdateAngularVelocity() {
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
        float angle;
        Vector3 axis;
        deltaRotation.ToAngleAxis(out angle, out axis);

        if (angle > 180) {
            angle -= 360;
        }

        Vector3 angularVelocity = axis * (angle / Time.fixedDeltaTime);
        angularVelocityBuffer.Dequeue();
        angularVelocityBuffer.Enqueue(angularVelocity);
        previousRotation = currentRotation;

        int amountHighSamples = (int)(samplePercentage * sampleCount);
        int amountLowSamples = sampleCount - amountHighSamples;
        highAngularVelocity = AverageVelocities(0, amountHighSamples);
        lowAngularVelocity = AverageVelocities(amountHighSamples + 1, amountLowSamples);
    }
    
    void Start() {
        // Fill buffer with zeroed vectors
        for (int i = 0; i < sampleCount; i++) {
            angularVelocityBuffer.Enqueue(Vector3.zero);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        UpdateAngularVelocity();

        cooldownTimer = Mathf.MoveTowards(cooldownTimer, 0.0f, Time.fixedDeltaTime);
        
        if (cooldownTimer == 0.0f) {   
            // Check for flick
            if (highAngularVelocity.magnitude >= highThreshold && lowAngularVelocity.magnitude <= lowThreshold) {
                cooldownTimer = cooldown;
                flickEvent.Invoke();
            }
        }
    }

}
