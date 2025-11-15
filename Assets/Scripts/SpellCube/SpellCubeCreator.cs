using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellCubeCreator : MonoBehaviour {
    [SerializeField] private GameObject spellCubePrefab;

    private Dictionary<SpellType, Material> cubeMaterials = new Dictionary<SpellType, Material>();

    void Awake() {
        var cubeMaterialsResource = Resources.Load<SpellCubeMaterials>("SpellCubeMaterials");
        var zipped = cubeMaterialsResource.spellTypes.Zip(cubeMaterialsResource.cubeMaterials, (type, mat) => (type, mat));
        foreach (var (type, mat) in zipped) {
            cubeMaterials[type] = mat;
        }
    }

    public GameObject CreateSpellCube(SpellEntry spellEntry, Transform parent) {
        GameObject spellCube = Instantiate(spellCubePrefab, parent.position, parent.rotation);

        SpellCube spellCubeScript = spellCube.GetComponent<SpellCube>();
        Material cubeMaterial = cubeMaterials[spellEntry.spellType];
        spellCubeScript.SetSpell(spellEntry, cubeMaterial);

        return spellCube;
    }
}
