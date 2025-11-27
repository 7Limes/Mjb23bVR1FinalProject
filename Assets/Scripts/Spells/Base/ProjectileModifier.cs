using UnityEngine;

abstract public class ProjectileModifier : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddModifier(this);
        base.AddToGroup(group);
    }
    
    // Applies to `projectile` once when it's created
    abstract public void ApplyInitial(GameObject projectile);

    // Applies to `projectile` every fixed update tick
    virtual public void ApplyContinuous(GameObject projectile) {}
}
