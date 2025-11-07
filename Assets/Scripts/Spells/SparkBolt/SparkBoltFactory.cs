using UnityEngine;

[CreateAssetMenu(fileName = "Spark Bolt Factory", menuName = "Scriptable Objects/SparkBoltFactory")]
public class SparkBoltFactory : ProjectileFactory {
    [SerializeField] private float gravity = 0.0f;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float minLifetime = 2.5f;
    [SerializeField] private float maxLifetime = 3.5f;

    public override void AddToGroup(SpellGroup group) {
        group.AddProjectile(this);
        group.DecrementCastable();
    }

    override public GameObject Cast(Transform castTransform) {
        GameObject obj = Instantiate(prefab);

        var script = obj.GetComponent<SparkBolt>();

        Vector3 projVelocity = castTransform.rotation * Vector3.forward * speed;
        script.Initialize(
            castTransform.position, castTransform.rotation,
            projVelocity, gravity, minLifetime, maxLifetime
        );

        return obj;
    }
}
