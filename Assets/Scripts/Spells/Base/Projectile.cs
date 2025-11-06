using UnityEngine;

abstract public class Projectile : MonoBehaviour {
    protected Vector3 velocity;
    protected float gravity;
    protected float lifetime;

    protected virtual void OnExpire() {
        Destroy(gameObject);
    }

    public void Initialize(Vector3 position, Quaternion rotation, Vector3 velocity, float gravity, float lifetime) {
        transform.position = position;
        transform.rotation = rotation;
        this.velocity = velocity;
        this.gravity = gravity;
        this.lifetime = lifetime;
    }

    void FixedUpdate() {
        velocity.y += gravity;
        transform.position += velocity;
        lifetime = Mathf.MoveTowards(lifetime, 0.0f, Time.fixedDeltaTime);
        if (lifetime == 0) {
            OnExpire();
        }
    }
}
