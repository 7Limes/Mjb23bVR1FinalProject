using UnityEngine;

public class SparkBolt : Projectile {
    public SpellGroup payloadGroup = null;

    void OnCollisionEnter(Collision collision) {
        if (IsInvulnerable()) {
            return;
        }
        if (collision.gameObject.CompareTag("Wand")) {
            return;
        }

        if (payloadGroup != null) {
            Vector3 normal = collision.GetContact(0).normal;
            Vector3 reflected = Vector3.Reflect(transform.forward, normal);
            Quaternion reflectedRotation = Quaternion.LookRotation(reflected);
            payloadGroup?.Cast(transform.position, reflectedRotation);
            Debug.Log("Casted payload");
        }

        OnExpire();
    }
}

