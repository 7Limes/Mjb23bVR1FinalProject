using UnityEngine;

[CreateAssetMenu(fileName = "Spell Entry", menuName = "Scriptable Objects/SpellEntry")]
public class SpellEntry : ScriptableObject {
    public string spellID;
    public Material iconMaterial;
    public SpellFactory spellFactory;

    public void AddToGroup(SpellGroup group) {
        spellFactory.AddToGroup(group);
    }
}