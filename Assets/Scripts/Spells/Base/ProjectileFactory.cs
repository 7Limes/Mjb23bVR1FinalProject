using UnityEngine;

abstract public class ProjectileFactory : SpellFactory {
    [SerializeField] protected GameObject prefab;
    abstract public GameObject Cast(Vector3 castPosition, Quaternion castRotation);
}

