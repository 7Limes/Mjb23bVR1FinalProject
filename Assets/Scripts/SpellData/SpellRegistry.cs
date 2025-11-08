using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpellRegistry", menuName = "Scriptable Objects/SpellRegistry")]
public class SpellRegistry : ScriptableObject {
    [SerializeField] private List<SpellEntry> spellEntries = new List<SpellEntry>();
    private Dictionary<string, SpellEntry> spellLookup;

    public void Initialize() {
        spellLookup = new Dictionary<string, SpellEntry>();
        for (int i = 0; i < spellEntries.Count; i++) {
            SpellEntry spellEntry = spellEntries[i];

            if (string.IsNullOrEmpty(spellEntry.spellID)) {
                Debug.LogError($"Spell entry at index {i} does not have an ID");
                continue;
            }

            if (spellEntry.iconMaterial == null) {
                Debug.LogError($"Spell entry {spellEntry.spellID} has not been assigned an icon");
            }
            if (spellEntry.spellFactory == null) {
                Debug.LogError($"Spell entry {spellEntry.spellID} has not been assigned a SpellFactory");
            }

            spellLookup[spellEntry.spellID] = spellEntry;
        }
    }

    public SpellEntry GetSpellEntry(string spellId) {
        if (spellLookup == null) {
            Initialize();
        }

        if (spellLookup.TryGetValue(spellId, out var spell)) {
            return spell;
        }

        Debug.LogError($"Could not find a SpellEntry with ID {spellId} in the registry. Maybe you forgot to add it?");
        return null;
    }
}