using UnityEngine;

public class StaticProjectile : MonoBehaviour {
    protected float lifetime;

    public void Initialize(Vector3 position, Quaternion rotation, float lifetime) {
        transform.position = position;
        transform.rotation = rotation;
        this.lifetime = lifetime;
    }

    protected virtual void OnExpire() {
        Destroy(gameObject);
    }

    void FixedUpdate() {
        lifetime = Mathf.MoveTowards(lifetime, 0.0f, Time.fixedDeltaTime);
        if (lifetime == 0) {
            OnExpire();
        }
    }

}
