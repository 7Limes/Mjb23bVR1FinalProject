using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrebuiltWand", menuName = "Scriptable Objects/PrebuiltWand")]
public class PrebuiltWand : ScriptableObject {
    public string wandName = "Unnamed Wand";

    [Header("Wand Stats")]
    public float castDelay = 0.5f;
    public float spread = 0.0f;
    public int capacity = 10;

    [Header("Spells")]
    public List<SpellEntry> spells;
}
