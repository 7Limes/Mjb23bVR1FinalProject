using UnityEngine;

abstract public class Modifier : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddModifier(this);
    }
    
    abstract public void Apply(GameObject projectile);
}
