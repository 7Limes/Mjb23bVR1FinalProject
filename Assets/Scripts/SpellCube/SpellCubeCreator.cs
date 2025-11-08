using UnityEngine;

public class SpellCubeCreator : MonoBehaviour {
    [SerializeField] private GameObject spellCubePrefab;

    private SpellRegistry registry;

    void Awake() {
        registry = Resources.Load<SpellRegistry>("SpellRegistry");
        registry.Initialize();
    }

    public GameObject CreateSpellCube(string spellId, Transform parent) {
        GameObject spellCube = Instantiate(spellCubePrefab, parent.position, parent.rotation);

        SpellCube spellCubeScript = spellCube.GetComponent<SpellCube>();
        SpellEntry spellEntry = registry.GetSpellEntry(spellId);
        spellCubeScript.SetSpell(spellEntry);

        return spellCube;
    }
}
