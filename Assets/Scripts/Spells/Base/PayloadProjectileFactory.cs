using UnityEngine;

[CreateAssetMenu(fileName = "Payload Projectile Factory", menuName = "Scriptable Objects/PayloadProjectileFactory")]
public class PayloadProjectileFactory : DynamicProjectileFactory {
    [Header("Payload Settings")]
    [Tooltip("Whether this projectile should capture a subgroup. (Enable for trigger spells)")]
    [SerializeField] private bool enableSubgroup = true;

    [Tooltip("Whether this projectile should deliver its payload on expiration.")]
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
        ApplyGeneralStatChanges(group);
        group.DecrementCastable();
    }

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<PayloadProjectile>();
        if (script == null) {
            Debug.LogError($"Could not find PayloadProjectile component on prefab {obj.name}");
            return null;
        }
        script.SetPayload(payloadGroup, deliverOnExpire);

        return obj;
    }
}
