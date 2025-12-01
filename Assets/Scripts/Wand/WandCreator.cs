using UnityEngine;

public class WandCreator : MonoBehaviour {
    [SerializeField] private GameObject wandPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float trackedDistance = 0.5f;

    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private Transform effectLocation;
    

    private WandModels wandModels;

    private GameObject currentWand;

    void Awake() {
        wandModels = Resources.Load<WandModels>("WandModels");
    }

    public GameObject CreateWand(int capacity, float castDelay, float spread) {
        currentWand = Instantiate(wandPrefab, spawnPoint.position, spawnPoint.rotation);

        GameObject wandModelPrefab = wandModels.GetRandomModel();
        GameObject wandModel = Instantiate(wandModelPrefab, currentWand.transform);
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Wand wandScript = currentWand.GetComponent<Wand>();
        if (wandScript != null) {
            wandScript.SetWandModel(wandModel);
            wandScript.capacity = capacity;
            wandScript.castDelay = castDelay;
            wandScript.spread = spread;
        }

        Instantiate(spawnEffectPrefab, effectLocation);

        return currentWand;
    }

    public GameObject GetCurrentWand() {
        return currentWand;
    }

    void FixedUpdate() {
        if (currentWand != null) {
            float wandDistance = Vector3.Distance(spawnPoint.position, currentWand.transform.position);
            if (wandDistance > trackedDistance) {
                currentWand = null;
            }
        }
    }
}