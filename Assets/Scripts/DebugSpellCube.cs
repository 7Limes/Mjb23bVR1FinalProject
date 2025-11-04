using UnityEngine;

public class DebugSpellCube : MonoBehaviour {

    [SerializeField] private Transform spawnPosition;
    private SpellCubeCreator spellCubeCreator;

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
    }

    public void SpawnSpellCube() {
        Debug.Log("Spawned new cube");
        spellCubeCreator.CreateSpellCube("spark_bolt", spawnPosition);
    }
}

