using UnityEngine;

[CreateAssetMenu(fileName = "Payload Projectile Factory", menuName = "Scriptable Objects/PayloadProjectileFactory")]
public class PayloadProjectileFactory : DynamicProjectileFactory {
    [SerializeField] private bool enableSubgroup = true;
    [SerializeField] private bool deliverOnExpire = false;

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
        script.SetPayload(payloadGroup, deliverOnExpire);

        return obj;
    }
}
