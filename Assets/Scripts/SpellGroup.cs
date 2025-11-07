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
        spellIndex = subGroup.spellIndex - 1;
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

    public void AddSpread(float spreadAmount) {
        spread += spreadAmount;
    }

    Quaternion RandomRotateWithSpread(Quaternion original, float spreadDegrees) {
        // Generate random rotation within a cone
        float angle = Random.Range(0f, spreadDegrees);
        float randomRotation = Random.Range(0f, 360f);

        // Create a random axis perpendicular to forward
        Vector3 axis = Quaternion.Euler(0, randomRotation, 0) * Vector3.right;

        // Create rotation around that axis
        Quaternion spread = Quaternion.AngleAxis(angle, axis);

        return spread * original;
    }


    public void Cast(Vector3 castPosition, Quaternion castRotation) {
        foreach (var projectile in projectiles) {
            Quaternion spreadRotation = RandomRotateWithSpread(castRotation, spread);
            projectile.Cast(castPosition, spreadRotation);
        }
    }
}

