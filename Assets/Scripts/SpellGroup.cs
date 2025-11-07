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

    public bool IsEmpty() {
        return projectiles.Count == 0;
    }

    public SpellGroup CreateSubgroup() {
        SpellGroup subGroup = new SpellGroup(spells, spellIndex+1);
        subGroup.Build();
        spellIndex = subGroup.spellIndex;
        return subGroup;
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

    public void Cast(Vector3 castPosition, Quaternion castRotation) {
        foreach (var projectile in projectiles) {
            projectile.Cast(castPosition, castRotation);
        }
    }
}

