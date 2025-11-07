using UnityEngine;

public class SparkTrigger : SparkBolt {
    SpellGroup payloadGroup = null;

    public void Initialize(SpellGroup payload) {
        payloadGroup = payload;
    }

    protected override void OnExpire() {
        payloadGroup?.Cast(transform.position, transform.rotation);
        base.OnExpire();
    }

    void OnExpire(Vector3 castPosition, Quaternion castRotation) {
        payloadGroup?.Cast(castPosition, castRotation);
        base.OnExpire();
    }

    void OnCollisionEnter(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        Vector3 reflected = Vector3.Reflect(transform.forward, normal);
        Quaternion reflectedRotation = Quaternion.LookRotation(reflected);
        OnExpire(transform.position, reflectedRotation);
    }
}

