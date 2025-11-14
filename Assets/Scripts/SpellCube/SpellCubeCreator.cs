using UnityEngine;

public class SpellCubeCreator : MonoBehaviour {
    [SerializeField] private GameObject spellCubePrefab;

    public GameObject CreateSpellCube(SpellEntry spellEntry, Transform parent) {
        GameObject spellCube = Instantiate(spellCubePrefab, parent.position, parent.rotation);

        SpellCube spellCubeScript = spellCube.GetComponent<SpellCube>();
        spellCubeScript.SetSpell(spellEntry);

        return spellCube;
    }
}
