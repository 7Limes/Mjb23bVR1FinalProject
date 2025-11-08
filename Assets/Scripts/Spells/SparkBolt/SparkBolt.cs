using UnityEngine;

public class SparkBolt : Projectile {
    public SpellGroup payloadGroup = null;
    private bool castedPayload = false;

    void OnCollisionEnter(Collision collision) {
        if (IsInvulnerable()) {
            return;
        }
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

