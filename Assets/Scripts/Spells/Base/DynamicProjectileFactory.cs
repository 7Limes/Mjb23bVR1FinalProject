using UnityEngine;

abstract public class DynamicProjectileFactory : ProjectileFactory {
    [SerializeField] protected float gravity = 0.0f;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected bool useRigidbodyPhysics = false;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<DynamicProjectile>();
        Vector3 projVelocity = castRotation * Vector3.forward * speed;
        script.Initialize(projVelocity, gravity, useRigidbodyPhysics);

        return obj;
    }
}

