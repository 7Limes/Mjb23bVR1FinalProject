using UnityEngine;

abstract public class ProjectileFactory : SpellFactory {
    [SerializeField] protected GameObject prefab;
    abstract public GameObject Cast(Transform castTransform);
}

