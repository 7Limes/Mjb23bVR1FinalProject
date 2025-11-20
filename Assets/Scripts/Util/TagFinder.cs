using UnityEngine;

public class TagFinder : MonoBehaviour {
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float detectionRadius = 10f;

    private GameObject nearestObject;

    void FixedUpdate() {
        nearestObject = FindNearestObject();
    }

    public GameObject FindNearestObject() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject nearest = null;
        float minDistance = detectionRadius;

        foreach (GameObject obj in objects) {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance <= detectionRadius && distance < minDistance) {
                minDistance = distance;
                nearest = obj;
            }
        }

        return nearest;
    }

    public GameObject GetNearestObject() {
        return nearestObject;
    }

    public void SetDetectionRadius(float radius) {
        detectionRadius = radius;
    }

    public void SetTargetTag(string tag) {
        targetTag = tag;
    }
}