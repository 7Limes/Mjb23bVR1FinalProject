using UnityEngine;

public class TeleportPlayer : Projectile {
    void Start() {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GameObject caster = GameObject.FindGameObjectWithTag("Player");
        if (caster != null) {
            caster.transform.position = transform.position;
        }
    }

    protected override void FixedUpdate() {
        Debug.Log("TeleportPlayer lifetime: " + lifetime);
        base.FixedUpdate();
    }
}

