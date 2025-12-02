using UnityEngine;
using System.Collections.Generic;

public class SpellGroup {
    const int MAX_COPIES = 200;

    private List<ProjectileFactory> projectiles;
    private List<ProjectileModifier> modifiers;

    private List<SpellEntry> spells;
    private int spellIndex;

    private int castableCount;
    private int copyCount;
    private int concurrentCastCount;

    private Vector2 spread;
    private float castDelay;

    void Initialize(List<SpellEntry> spellList, int startIndex, int castables) {
        projectiles = new List<ProjectileFactory>();
        modifiers = new List<ProjectileModifier>();
        spells = spellList;
        spellIndex = startIndex;
        castableCount = castables;
        copyCount = 1;
        spread = Vector2.zero;
        castDelay = 0;
        concurrentCastCount = 1;
    }

    public SpellGroup() {
        Initialize(new List<SpellEntry>(), 0, 0);
    }

    public SpellGroup(List<SpellEntry> spellList, int startIndex) {
        Initialize(spellList, startIndex, 1);
    }

    public void Build() {
        while (castableCount > 0 && spellIndex < spells.Count) {
            SpellEntry spell = spells[spellIndex];
            if (spell != null) {
                int copyIterations = copyCount-1;
                copyCount = 0;
                int savedCastableCount = castableCount;
                int savedSpellIndex = spellIndex;

                // Cast n-1 copies of the spell
                for (int i = 0; i < copyIterations; i++) {
                    spell.AddToGroup(this);
                    spellIndex = savedSpellIndex;
                }
                castableCount = savedCastableCount;

                // Cast a final spell to ensure all fields are normal
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

    public void AddModifier(ProjectileModifier mod) {
        modifiers.Add(mod);
    }

    public void AddCopies(int copyAmount) {
        copyCount += copyAmount;
        copyCount = Mathf.Clamp(copyCount, 1, MAX_COPIES);
    }

    public int GetConcurrentCasts() {
        return concurrentCastCount;
    }

    public void AddConcurrentCasts(int amount) {
        concurrentCastCount += amount;
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

    public void Cast(Vector3 castPosition, Quaternion castRotation, Vector2 addedSpread) {
        Vector2 totalSpread = spread + addedSpread;
        foreach (var projectile in projectiles) {
            Quaternion spreadRotation = RandomRotateWithSpread(castRotation, totalSpread);
            GameObject projectileObj = projectile.Cast(castPosition, spreadRotation);
            foreach (var modifier in modifiers) {
                modifier.ApplyInitial(projectileObj);
            }
            projectileObj.GetComponent<Projectile>().SetModifiers(modifiers);
        }
    }

    public void Extend(SpellGroup other) {
        projectiles.AddRange(other.projectiles);
    }
}

