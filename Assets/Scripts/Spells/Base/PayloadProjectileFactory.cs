using UnityEngine;

[CreateAssetMenu(fileName = "Spark Trigger Factory", menuName = "Scriptable Objects/SparkTriggerFactory")]
public class PayloadProjectileFactory : DynamicProjectileFactory {
    [SerializeField] private bool enableSubgroup = true;
    protected SpellGroup payloadGroup = null;

    public override void AddToGroup(SpellGroup group) {
        var factory = (PayloadProjectileFactory)MemberwiseClone();

        SpellGroup payload;
        if (enableSubgroup) {
            payload = group.CreateSubgroup();
        }
        else {
            payload = new SpellGroup();
        }
        
        if (payloadGroup != null) {
            payload.Extend(payloadGroup);
            payloadGroup = null;
        }
        factory.payloadGroup = payload;

        group.AddProjectile(factory);
        group.DecrementCastable();
    }

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<PayloadProjectile>();
        script.SetPayload(payloadGroup);

        return obj;
    }
}
