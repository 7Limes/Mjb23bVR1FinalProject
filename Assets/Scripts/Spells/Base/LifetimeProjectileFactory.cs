using UnityEngine;

abstract public class LifetimeProjectileFactory : ProjectileFactory {
    [SerializeField] protected float minLifetime = 1.0f;
    [SerializeField] protected float maxLifetime = 1.5f;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<Projectile>();
        float newLifetime = Random.Range(minLifetime, maxLifetime);
        script.SetLifetime(newLifetime);

        return obj;
    }
}

