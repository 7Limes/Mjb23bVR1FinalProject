using UnityEngine;

public class TeleportBolt : DynamicProjectile {
    protected override void OnExpire() {
        GameObject caster = GameObject.FindGameObjectWithTag("Player");
        if (caster != null) {
            caster.transform.position = transform.position;
        }

        base.OnExpire();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wand")) {
            return;
        }

        OnExpire();
    }
}

