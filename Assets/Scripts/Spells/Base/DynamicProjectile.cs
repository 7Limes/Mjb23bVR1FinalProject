using UnityEngine;

abstract public class DynamicProjectile : Projectile {
    protected Vector3 velocity;
    protected float gravity;
    protected Rigidbody rb;

    public void Initialize(Vector3 velocity, float gravity, float lifetime) {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Could not find Rigidbody on dynamic projectile. Please add one.");
            return;
        }

        this.velocity = velocity;
        this.gravity = gravity;
        this.lifetime = lifetime;
    }

    protected override void FixedUpdate() {
        velocity.y += gravity * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;

        base.FixedUpdate();
    }
}
