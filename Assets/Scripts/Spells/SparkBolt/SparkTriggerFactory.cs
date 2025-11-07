using UnityEngine;

[CreateAssetMenu(fileName = "Spark Trigger Factory", menuName = "Scriptable Objects/SparkTriggerFactory")]
public class SparkTriggerFactory : SparkBoltFactory {
    SpellGroup payloadGroup = null;

    public override void AddToGroup(SpellGroup group) {
        var factory = (SparkTriggerFactory)MemberwiseClone();
        payloadGroup = group.CreateSubgroup();
        factory.payloadGroup = payloadGroup;
        group.AddProjectile(factory);
        group.DecrementCastable();
    }

    override public GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        SparkBolt script = obj.GetComponent<SparkBolt>();
        script.SetPayload(payloadGroup);

        return obj;
    }
}
