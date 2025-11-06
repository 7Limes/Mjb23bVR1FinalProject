using UnityEngine;

[CreateAssetMenu(fileName = "Spark Bolt Factory", menuName = "Scriptable Objects/SparkBoltFactory")]
public class SparkBoltFactory : ProjectileFactory {
    const float GRAVITY = 1.0f;
    const float SPEED = 0.5f;
    const float MIN_LIFETIME = 2.5f;
    const float MAX_LIFETIME = 3.5f;

    public override void AddToGroup(SpellGroup group) {
        Debug.Log("Added new spark bolt to group!");
        var factory = CreateInstance<SparkBoltFactory>();
        factory.prefab = prefab;
        group.AddProjectile(factory);
        group.DecrementCastable();
    }

    override public GameObject Cast(Transform castTransform) {
        GameObject obj = Instantiate(prefab);

        var script = obj.GetComponent<SparkBolt>();

        Vector3 projVelocity = castTransform.rotation * Vector3.forward * SPEED;
        script.Initialize(
            castTransform.position, castTransform.rotation,
            projVelocity, GRAVITY, MIN_LIFETIME, MAX_LIFETIME
        );

        return obj;
    }
}
