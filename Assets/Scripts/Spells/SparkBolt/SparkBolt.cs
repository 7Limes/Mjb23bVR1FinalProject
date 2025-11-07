using UnityEngine;

public class SparkBolt : LifetimeProjectile {
    protected override void OnExpire() {
        // TODO: summon small explosion here
        base.OnExpire();
    }

    void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Wand")) {
            OnExpire();
        }
    }
}

