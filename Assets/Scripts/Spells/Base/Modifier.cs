using UnityEngine;

abstract public class Modifier : SpellFactory {
    abstract public void Apply(GameObject projectile);
}
