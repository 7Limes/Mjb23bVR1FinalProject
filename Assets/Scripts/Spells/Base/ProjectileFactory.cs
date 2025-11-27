using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFactory", menuName = "Scriptable Objects/ProjectileFactory")]
public class ProjectileFactory : SpellFactory {
    [Header("Projectile Settings")]
    [Tooltip("The prefab of the projectile.")]
    [SerializeField] private GameObject prefab;

    [Tooltip("The base lifetime of the projectile in seconds.")]
    [SerializeField] protected float lifetime = 1.0f;

    [Tooltip("The variance to add/subtract from the lifetime of the projectile.")]
    [SerializeField] protected float lifetimeVariance = 0.0f;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
        base.AddToGroup(group);
    }

    public virtual GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(prefab, castPosition, castRotation);

        var script = obj.GetComponent<Projectile>();
        float newLifetime = Random.Range(lifetime-lifetimeVariance, lifetime+lifetimeVariance);
        script.Initialize(newLifetime);

        return obj;
    }
}

