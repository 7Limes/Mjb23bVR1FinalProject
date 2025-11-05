using UnityEngine;

public class DebugSpellCube : MonoBehaviour {

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private string spellId = "spark_bolt";
    
    private SpellCubeCreator spellCubeCreator;

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
    }

    public void SpawnSpellCube() {
        spellCubeCreator.CreateSpellCube(spellId, spawnPosition);
    }
}

