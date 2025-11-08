using System;
using UnityEngine;

abstract public class Projectile : MonoBehaviour {
    protected float gravity;
    protected float lifetime;
    protected Rigidbody rb;
    protected Vector3 velocity;

    protected int invulnerableTicks = 5;

    protected virtual void OnExpire() {
        if (!IsInvulnerable()) {
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 position, Quaternion rotation, Vector3 velocity, float gravity, float lifetime) {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Could not find Rigidbody on projectile. Please add one.");
            return;
        }

        transform.position = position;
        transform.rotation = rotation;
        this.velocity = velocity;
        this.gravity = gravity;
        this.lifetime = lifetime;
    }

    public bool IsInvulnerable() {
        return invulnerableTicks > 0;
    }

    public void SetLifetime(float newLifetime) {
        lifetime = newLifetime;
    }

    void FixedUpdate() {
        if (invulnerableTicks > 0) {
            invulnerableTicks -= 1;
        }

        velocity.y += gravity * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;


        lifetime = Mathf.MoveTowards(lifetime, 0.0f, Time.fixedDeltaTime);
        if (lifetime == 0) {
            OnExpire();
        }
    }
}
