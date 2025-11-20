using UnityEngine;
using System.Collections.Generic;

public class WandSpawner : MonoBehaviour {
    [SerializeField] private int minCapacity = 2;
    [SerializeField] private int maxCapacity = 10;

    [SerializeField] private float minCastDelay = 0.1f;
    [SerializeField] private float maxCastDelay = 0.75f;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject wandPrefab;
    [SerializeField] private List<GameObject> wandModelPrefabs;

    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private Transform effectLocation;

    [SerializeField] private float spawnDistance = 0.15f;
    [SerializeField] private float spawnDelay = 1.0f;

    private GameObject currentWand = null;
    private float spawnTimer = 0.0f;

    void Start() {
        SpawnNewWand();
    }

    void SpawnNewWand() {
        if (wandModelPrefabs == null || wandModelPrefabs.Count == 0) {
            Debug.LogWarning("Wand model list is empty.");
            return;
        }

        currentWand = Instantiate(wandPrefab, spawnPoint.position, spawnPoint.rotation);

        // Create wand model
        int modelIndex = Random.Range(0, wandModelPrefabs.Count);
        GameObject wandModelPrefab = wandModelPrefabs[modelIndex];
        GameObject wandModel = Instantiate(wandModelPrefab, currentWand.transform);
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Wand wandScript = currentWand.GetComponent<Wand>();
        if (wandScript != null) {
            wandScript.SetWandModel(wandModel);
            wandScript.SetCapacity(Random.Range(minCapacity, maxCapacity+1));
            wandScript.SetCastDelay(Random.Range(minCastDelay, maxCastDelay));
        }

        Instantiate(spawnEffectPrefab, effectLocation);
    }

    void FixedUpdate() {
        if (currentWand != null) {
            float wandDistance = Vector3.Distance(spawnPoint.position, currentWand.transform.position);
            if (wandDistance > spawnDistance) {
                currentWand = null;
                spawnTimer = spawnDelay;
            }
        }

        if (spawnTimer > 0) {
            spawnTimer = Mathf.MoveTowards(spawnTimer, 0.0f, Time.fixedDeltaTime);
            if (spawnTimer == 0) {
                SpawnNewWand();
            }
        }
    }
}
