using UnityEngine;

public class TeleportBolt : Projectile {
    protected override void OnExpire() {
        GameObject caster = GameObject.FindGameObjectWithTag("Player");
        if (caster != null) {
            caster.transform.position = transform.position;
        }

        base.OnExpire();
    }

    void OnCollisionEnter(Collision collision) {
        if (IsInvulnerable()) {
            return;
        }
        if (collision.gameObject.CompareTag("Wand")) {
            return;
        }

        OnExpire();
    }
}

