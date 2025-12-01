using UnityEngine;

public class TeleportPlayer : Projectile {
    void Start() {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GameObject caster = GameObject.FindGameObjectWithTag("XRRig");
        if (caster != null) {
            caster.transform.position = transform.position;
        }
    }
}

