using UnityEngine;

public class SpellCubeStand : MonoBehaviour {

    [SerializeField] private SpellEntry spellEntry;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnDistance = 0.15f;
    
    private SpellCubeCreator spellCubeCreator;

    private GameObject currentCube = null;

    GameObject SpawnCube() {
        return spellCubeCreator.CreateSpellCube(spellEntry, spawnPosition);
    }

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
        currentCube = SpawnCube();
    }

    void FixedUpdate() {
        float cubeDistance = Vector3.Distance(spawnPosition.position, currentCube.transform.position);
        if (cubeDistance > spawnDistance) {
            currentCube.GetComponent<SpellCube>().SetSuspended(false);
            currentCube = SpawnCube();
        }
    }
}

