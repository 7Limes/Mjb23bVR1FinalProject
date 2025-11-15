using UnityEngine;

public enum SpellType {Projectile, Modifier, StaticProjectile, Multicast};

[CreateAssetMenu(fileName = "Spell Entry", menuName = "Scriptable Objects/SpellEntry")]
public class SpellEntry : ScriptableObject {
    public string spellID;
    public Material iconMaterial;
    public SpellFactory spellFactory;
    public SpellType spellType;

    public string spellName;

    [TextArea(3, 10)]
    public string spellDescription;

    public void AddToGroup(SpellGroup group) {
        spellFactory.AddToGroup(group);
    }
}