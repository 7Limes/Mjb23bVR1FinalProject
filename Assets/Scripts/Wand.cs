using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Wand : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 0.25f;
    [SerializeField] private float oscillateSpeed = 1.0f;
    [SerializeField] private float oscillateAmplitude = 0.05f;

    private GameObject wandModel;
    private bool isGrabbed = false;

    public void SetWandModel(GameObject wandModel) {
        this.wandModel = wandModel;
    }
    
    public void OnGrab() {
        isGrabbed = true;
    }

    public void OnRelease() {
        isGrabbed = false;
        wandModel.transform.localPosition = Vector3.zero;
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update() {
        if (!isGrabbed) {
            Vector3 rotateVector = new Vector3(0, rotateSpeed, 0);
            wandModel.transform.Rotate(rotateVector);

            float oscillateY = oscillateAmplitude * Mathf.Sin(Time.fixedTime * oscillateSpeed);
            Vector3 oscillatePosition = new Vector3(0, oscillateY, 0);
            wandModel.transform.localPosition = oscillatePosition;
        }
    }
}
