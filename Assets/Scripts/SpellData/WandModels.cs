using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WandModels", menuName = "Scriptable Objects/WandModels")]
public class WandModels : ScriptableObject {
    public List<GameObject> wandModelPrefabs;

    void Awake() {
        if (wandModelPrefabs == null || wandModelPrefabs.Count == 0) {
            Debug.LogWarning("WandModels: Wand model list is empty.");
            return;
        }
    }

    public GameObject GetRandomModel() {
        int modelIndex = Random.Range(0, wandModelPrefabs.Count);
        return wandModelPrefabs[modelIndex];
    }
}
