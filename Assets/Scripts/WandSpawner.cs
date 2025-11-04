using UnityEngine;
using System.Collections.Generic;

public class WandSpawner : MonoBehaviour {
    [SerializeField] private GameObject wandPrefab;
    [SerializeField] private List<GameObject> wandModelPrefabs;

    private Transform spawnPoint;

    void Start() {
        spawnPoint = GetComponent<Transform>();
        SpawnNewWand();
    }

    void SpawnNewWand() {
        if (wandModelPrefabs == null || wandModelPrefabs.Count == 0) {
            Debug.LogWarning("Wand model list is empty.");
            return;
        }

        GameObject wand = Instantiate(wandPrefab, spawnPoint.position, spawnPoint.rotation);

        // Create wand model
        int modelIndex = Random.Range(0, wandModelPrefabs.Count);
        GameObject wandModelPrefab = wandModelPrefabs[modelIndex];
        GameObject wandModel = Instantiate(wandModelPrefab, wand.transform);
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Wand wandScript = wand.GetComponent<Wand>();
        if (wandScript != null) {
            wandScript.SetWandModel(wandModel);
        }
    }
    
    void Update() {
        
    }
}
