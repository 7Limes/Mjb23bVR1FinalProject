using UnityEngine;
using System.Collections.Generic;

public class SpellGroup {
    private List<ProjectileFactory> projectiles;

    private List<SpellEntry> spells;
    private int spellIndex;

    private int castableCount;

    private Vector2 spread;
    private float castDelay;

    public SpellGroup() {
        projectiles = new List<ProjectileFactory>();
        spells = new List<SpellEntry>();
        spellIndex = 0;
        castableCount = 0;
        spread = Vector2.zero;
        castDelay = 0;
    }

    public SpellGroup(List<SpellEntry> spellList, int startIndex) {
        projectiles = new List<ProjectileFactory>();
        spells = spellList;
        spellIndex = startIndex;
        castableCount = 1;
        spread = Vector2.zero;
        castDelay = 0;
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

    // Getters
    public int GetIndex() {
        return spellIndex;
    }

    public float GetCastDelay() {
        return castDelay;
    }

    // Public methods
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

    public void AddSpread(Vector2 amount) {
        spread += amount;
    }

    public void AddSpread(float amount) {
        spread.x += amount;
        spread.y += amount;
    }

    public void AddCastDelay(float amount) {
        castDelay += amount;
    }

    Quaternion RandomRotateWithSpread(Quaternion original, Vector2 spread) {
        spread.x = Mathf.Clamp(spread.x, 0, 360);
        spread.y = Mathf.Clamp(spread.y, 0, 360);
        float horizontalAngle = Random.Range(-spread.x, spread.x);
        float verticalAngle = Random.Range(-spread.y, spread.y);

        Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalAngle, Vector3.up);
        Quaternion verticalRotation = Quaternion.AngleAxis(verticalAngle, Vector3.right);

        return original * horizontalRotation * verticalRotation;
    }

    public void Cast(Vector3 castPosition, Quaternion castRotation) {
        foreach (var projectile in projectiles) {
            Quaternion spreadRotation = RandomRotateWithSpread(castRotation, spread);
            projectile.Cast(castPosition, spreadRotation);
        }
    }

    public void Extend(SpellGroup other) {
        projectiles.AddRange(other.projectiles);
    }
}

