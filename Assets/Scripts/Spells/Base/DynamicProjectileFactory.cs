using UnityEngine;

[CreateAssetMenu(fileName = "Dynamic Projectile Factory", menuName = "Scriptable Objects/DynamicProjectileFactory")]
public class DynamicProjectileFactory : ProjectileFactory {
    [SerializeField] protected float gravity = 0.0f;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected bool expireOnCollision = true;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<DynamicProjectile>();
        Vector3 projVelocity = castRotation * Vector3.forward * speed;
        script.Initialize(projVelocity, gravity, expireOnCollision);

        return obj;
    }
}

