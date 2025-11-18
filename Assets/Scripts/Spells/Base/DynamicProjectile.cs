using UnityEngine;

abstract public class DynamicProjectile : Projectile {
    protected float gravity;
    public Rigidbody rb;

    public void Initialize(Vector3 velocity, float gravity) {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Could not find Rigidbody on dynamic projectile. Please add one.");
            return;
        }

        this.gravity = gravity;
        rb.linearVelocity = velocity;
    }

    protected override void FixedUpdate() {
        if (!rb.isKinematic) {
            rb.linearVelocity += new Vector3(0, gravity, 0);
        }

        base.FixedUpdate();
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("ProjectileNoCollide")) {
            return;
        }

        OnExpire();
    }
}
