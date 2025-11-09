using UnityEngine;

public class PayloadProjectile : DynamicProjectile {
    private SpellGroup payloadGroup = null;
    private bool castedPayload = false;

    public void SetPayload(SpellGroup group) {
        payloadGroup = group;
    }

    public void ExtendPayload(SpellGroup group) {
        payloadGroup.Extend(group);
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("ProjectileNoCollide")) {
            return;
        }


        if (payloadGroup != null && !castedPayload) {
            ContactPoint contact = collision.GetContact(0);
            Vector3 normal = contact.normal;
            Vector3 reflected = Vector3.Reflect(transform.forward, normal);
            Quaternion reflectedRotation = Quaternion.LookRotation(reflected);

            // Vector3 castPosition = contact.point + normal * 0.05f;
            payloadGroup?.Cast(transform.position, reflectedRotation);
            castedPayload = true;
            Debug.Log("Casted payload");
        }

        OnExpire();
    }
}
