using UnityEngine;

public class WandCreator : MonoBehaviour {
    [SerializeField] private GameObject wandPrefab;

    private WandModels wandModels;

    void Awake() {
        wandModels = Resources.Load<WandModels>("WandModels");
        
    }

    public GameObject CreateWand(Vector3 position, Quaternion rotation, int capacity, float castDelay, float spread) {
        GameObject wand = Instantiate(wandPrefab, position, rotation);

        GameObject wandModelPrefab = wandModels.GetRandomModel();
        GameObject wandModel = Instantiate(wandModelPrefab, wand.transform);
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Wand wandScript = wand.GetComponent<Wand>();
        if (wandScript != null) {
            wandScript.SetWandModel(wandModel);
            wandScript.capacity = capacity;
            wandScript.castDelay = castDelay;
            wandScript.spread = spread;
        }

        return wand;
    }
}