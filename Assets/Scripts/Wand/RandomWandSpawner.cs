using UnityEngine;
using System.Collections.Generic;

public class RandomWandSpawner : MonoBehaviour {
    [SerializeField] private Vector2 capacityRange = new Vector2(2, 10);
    [SerializeField] private Vector2 castDelayRange = new Vector2(0.1f, 0.75f);
    [SerializeField] private Vector2 spreadRange = new Vector2(0.0f, 5.0f);

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private Transform effectLocation;

    [SerializeField] private float spawnDistance = 0.15f;
    [SerializeField] private float spawnDelay = 1.0f;

    private WandCreator wandCreator;

    private GameObject currentWand = null;
    private float spawnTimer = 0.0f;


    void Awake() {
        wandCreator = GetComponent<WandCreator>();
    }

    void Start() {
        SpawnNewWand();
    }

    void SpawnNewWand() {
        int capacity = (int) Random.Range(capacityRange.x, capacityRange.y+1);
        float castDelay = Random.Range(castDelayRange.x, castDelayRange.y);
        float spread = Random.Range(spreadRange.x, spreadRange.y);
        currentWand = wandCreator.CreateWand(spawnPoint.position, spawnPoint.rotation, capacity, castDelay, spread);
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
