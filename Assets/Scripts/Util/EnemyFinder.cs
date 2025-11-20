using UnityEngine;

public class EnemyFinder : MonoBehaviour {
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 10f;

    private GameObject nearestEnemy;

    void FixedUpdate() {
        nearestEnemy = FindNearestEnemy();
    }

    public GameObject FindNearestEnemy() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = detectionRadius;

        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius && distance < minDistance) {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public GameObject GetNearestEnemy() {
        return nearestEnemy;
    }

    public void SetDetectionRadius(float radius) {
        detectionRadius = radius;
    }
}