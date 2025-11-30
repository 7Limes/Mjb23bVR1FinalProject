using UnityEngine;

public class PayloadProjectile : DynamicProjectile {
    private SpellGroup payloadGroup = null;
    private bool deliverOnExpire = false;
    private bool castedPayload = false;

    public void SetPayload(SpellGroup group, bool shouldDeliverOnExpire) {
        payloadGroup = group;
        deliverOnExpire = shouldDeliverOnExpire;
    }

    public void ExtendPayload(SpellGroup group) {
        payloadGroup.Extend(group);
    }

    protected void CastPayload(Vector3 castPosition, Quaternion castRotation) {
        if (payloadGroup != null && !castedPayload) {
            
            payloadGroup.Cast(castPosition, castRotation, Vector2.zero);
            castedPayload = true;
        }
    }

    protected override void OnExpire() {
        if (deliverOnExpire) {
            CastPayload(transform.position, transform.rotation);
        }
        
        base.OnExpire();
    }

    protected override void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.GetContact(0);
        Vector3 normal = contact.normal;
        Vector3 castPosition = contact.point + normal * 0.2f;

        Vector3 reflected = Vector3.Reflect(transform.forward, normal);
        Quaternion reflectedRotation = Quaternion.LookRotation(reflected);

        CastPayload(castPosition, reflectedRotation);
        
        base.OnCollisionEnter(collision);
    }
}
