using UnityEngine;

abstract public class DynamicProjectile : Projectile {
    protected Vector3 velocity;
    protected float gravity;
    protected Rigidbody rb;
    protected bool useRigidbodyPhysics;

    public void Initialize(Vector3 velocity, float gravity, bool useRigidbodyPhysics) {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Could not find Rigidbody on dynamic projectile. Please add one.");
            return;
        }

        this.velocity = velocity;
        this.gravity = gravity;
        this.useRigidbodyPhysics = useRigidbodyPhysics;
        if (useRigidbodyPhysics) {
            rb.linearVelocity = velocity;
        }
    }

    protected override void FixedUpdate() {
        if (useRigidbodyPhysics) {
            if (!rb.isKinematic) {
                rb.linearVelocity += new Vector3(0, gravity, 0);
            }
        }
        else {
            velocity.y += gravity * Time.fixedDeltaTime;
            transform.position += velocity * Time.fixedDeltaTime;
        }

        base.FixedUpdate();
    }
}
