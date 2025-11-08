using UnityEngine;

[CreateAssetMenu(fileName = "Spark Bolt Factory", menuName = "Scriptable Objects/SparkBoltFactory")]
public class SparkBoltFactory : ProjectileFactory {
    [SerializeField] private float gravity = 0.0f;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float minLifetime = 2.5f;
    [SerializeField] private float maxLifetime = 3.5f;

    [SerializeField] private GameObject hitEffectPrefab;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
    }

    override public GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(prefab);

        SpellGroup group = new SpellGroup();
        EffectFactory effectSpell = CreateInstance<EffectFactory>();
        effectSpell.SetEffectPrefab(hitEffectPrefab);
        group.AddProjectile(effectSpell);

        Vector3 projVelocity = castRotation * Vector3.forward * speed;
        SparkBolt script = obj.AddComponent<SparkBolt>();
        script.Initialize(
            castPosition, castRotation,
            projVelocity, gravity, minLifetime, maxLifetime,
            group
        );

        return obj;
    }
}
