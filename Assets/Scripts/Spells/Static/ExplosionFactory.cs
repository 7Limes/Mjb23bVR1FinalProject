using UnityEngine;
[CreateAssetMenu(fileName = "Explosion Factory", menuName = "Scriptable Objects/ExplosionFactory")]

public class ExplosionFactory : ProjectileFactory {
    [SerializeField] private float lifetime = 3.0f;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
    }

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(prefab);

        var script = obj.GetComponent<StaticProjectile>();
        script.Initialize(castPosition, castRotation, lifetime);

        return obj;
    }
}
