using UnityEngine;

[CreateAssetMenu(fileName = "Spark Trigger Factory", menuName = "Scriptable Objects/SparkTriggerFactory")]
public class SparkTriggerFactory : SparkBoltFactory {
    SpellGroup payloadGroup = null;

    public override void AddToGroup(SpellGroup group) {
        var factory = (SparkTriggerFactory)MemberwiseClone();
        payloadGroup = group.CreateSubgroup();
        group.AddProjectile(factory);
        group.DecrementCastable();
    }

    override public GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        Destroy(obj.GetComponent<SparkBolt>());
        SparkTrigger script = obj.AddComponent<SparkTrigger>();
        script.Initialize(payloadGroup);

        return obj;
    }
}
