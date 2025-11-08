using UnityEngine;

public class LifetimeProjectile : Projectile {
    public void Initialize(Vector3 position, Quaternion rotation, Vector3 velocity, float gravity, float minLifetime, float maxLifetime) {
        float newLifetime = Random.Range(minLifetime, maxLifetime);
        Initialize(position, rotation, velocity, gravity, newLifetime);
    }
}