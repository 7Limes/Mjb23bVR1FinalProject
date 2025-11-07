using UnityEngine;

public class SparkBolt : LifetimeProjectile {
    SpellGroup payloadGroup = null;

    public void Initialize(Vector3 position, Quaternion rotation, Vector3 velocity, float gravity, float minLifetime, float maxLifetime, SpellGroup payload) {
        Initialize(position, rotation, velocity, gravity, minLifetime, maxLifetime);
        payloadGroup = payload;
    }

    public void SetPayload(SpellGroup payload) {
        payloadGroup = payload;
    }

    protected override void OnExpire() {
        // TODO: summon small explosion here
        base.OnExpire();
    }

    void OnCollisionEnter(Collision collision) {
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

