using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFactory", menuName = "Scriptable Objects/ProjectileFactory")]
public class ProjectileFactory : SpellFactory {
    [SerializeField] private GameObject prefab;
    [SerializeField] protected float lifetime = 1.0f;
    [SerializeField] protected float lifetimeVariance = 0.0f;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
    }

    public virtual GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(prefab, castPosition, castRotation);

        var script = obj.GetComponent<Projectile>();
        float newLifetime = Random.Range(lifetime-lifetimeVariance, lifetime+lifetimeVariance);
        script.Initialize(newLifetime);

        return obj;
    }
}

