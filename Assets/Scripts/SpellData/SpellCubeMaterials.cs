using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "SpellCubeMaterials", menuName = "Scriptable Objects/SpellCubeMaterials")]
public class SpellCubeMaterials : ScriptableObject {
    public List<SpellType> spellTypes = new List<SpellType>();
    public List<Material> cubeMaterials = new List<Material>();
}
