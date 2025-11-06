using UnityEngine;
using System.Collections.Generic;

public class SpellGroup {
    private List<ProjectileFactory> projectiles;

    private List<SpellEntry> spells;
    private int spellIndex;

    private int castableCount;
    private float spread;

    public SpellGroup(List<SpellEntry> spells, int startIndex) {
        projectiles = new List<ProjectileFactory>();
        this.spells = spells;
        spellIndex = startIndex;
        castableCount = 1;
        spread = 0.0f;
    }

    public void Build() {
        while (castableCount > 0 && spellIndex < spells.Count) {
            SpellEntry spell = spells[spellIndex];
            if (spell != null) {
                spell.AddToGroup(this);
            }
            spellIndex++;
        }
    }

    public int GetIndex() {
        return spellIndex;
    }

    public SpellGroup CreateChild() {
        SpellGroup childGroup = new SpellGroup(spells, spellIndex);
        childGroup.Build();
        spellIndex = childGroup.spellIndex;
        return childGroup;
    }

    public void AddProjectile(ProjectileFactory proj) {
        projectiles.Add(proj);
    }

    public void AddMulticast(int multicastAmount) {
        castableCount += multicastAmount;
    }

    public void DecrementCastable() {
        castableCount -= 1;
    }

    public void AddSpread(int spreadAmount) {
        spread += spreadAmount;
    }

    public void Cast(Transform castTransform) {
        foreach (var projectile in projectiles) {
            projectile.Cast(castTransform);
        }
    }
}

