using UnityEngine;

public class DebugSpellCube : MonoBehaviour {

    [SerializeField] private Transform spawnPosition;
    private SpellCubeCreator spellCubeCreator;

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
    }

    public void SpawnSpellCube() {
        spellCubeCreator.CreateSpellCube("spark_bolt", spawnPosition);
    }
}

