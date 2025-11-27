using UnityEngine;

[CreateAssetMenu(fileName = "Dynamic Projectile Factory", menuName = "Scriptable Objects/DynamicProjectileFactory")]
public class DynamicProjectileFactory : ProjectileFactory {
    [Tooltip("The force of gravity to apply to the projectile.")]
    [SerializeField] protected float gravity = 0.0f;

    [Tooltip("The initial speed of the projectile.")]
    [SerializeField] protected float speed = 10.0f;

    [Tooltip("Whether the projectile should call OnExpire when it collides with something.")]
    [SerializeField] protected bool expireOnCollision = true;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<DynamicProjectile>();
        if (script == null) {
            Debug.LogError($"Could not find DynamicProjectile component on prefab {obj.name}");
            return null;
        }

        Vector3 projVelocity = castRotation * Vector3.forward * speed;
        script.Initialize(projVelocity, gravity, expireOnCollision);

        return obj;
    }
}

