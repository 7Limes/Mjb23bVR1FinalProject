using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpellRegistry", menuName = "Scriptable Objects/SpellRegistry")]
public class SpellRegistry : ScriptableObject {
    [SerializeField] private List<SpellEntry> spellEntries = new List<SpellEntry>();
    private Dictionary<string, SpellEntry> spellLookup;

    public void Initialize() {
        spellLookup = new Dictionary<string, SpellEntry>();
        foreach (var spellEntry in spellEntries) {
            if (!string.IsNullOrEmpty(spellEntry.spellID)) {
                spellLookup[spellEntry.spellID] = spellEntry;
            }
        }
    }

    public SpellEntry GetSpellEntry(string spellId) {
        if (spellLookup == null) {
            Initialize();
        }

        return spellLookup.TryGetValue(spellId, out var spell) ? spell : null;
    }
}