using UnityEngine;

abstract public class ProjectileFactory : SpellFactory {
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float gravity = 0.0f;
    [SerializeField] protected float speed = 10.0f;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
    }

    public virtual GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(prefab);

        var script = obj.GetComponent<Projectile>();
        Vector3 projVelocity = castRotation * Vector3.forward * speed;
        script.Initialize(castPosition, castRotation, projVelocity, gravity, 1.0f);

        return obj;
    }
}

