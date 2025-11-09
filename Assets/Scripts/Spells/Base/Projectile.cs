using UnityEngine;

public class Projectile : MonoBehaviour {
    protected float lifetime;
    protected virtual void OnExpire() {
        Destroy(gameObject);
    }

    public void Initialize(float lifetime) {
        this.lifetime = lifetime;
    }

    public void SetLifetime(float newLifetime) {
        lifetime = newLifetime;
    }

    protected virtual void FixedUpdate() {
        if (lifetime != -1) {
            lifetime = Mathf.MoveTowards(lifetime, 0.0f, Time.fixedDeltaTime);
            if (lifetime == 0) {
                OnExpire();
            }
        }
    }
}
