using UnityEngine;

public class SpitterBolt : PayloadProjectile {
    private float baseLifetime = 0.0f;
    private float baseScale = 1.0f;

    void Start() {
        baseLifetime = lifetime;
        baseScale = transform.localScale.x;
    }

    protected override void FixedUpdate() {
        float t = Mathf.InverseLerp(0.0f, baseLifetime, lifetime);
        float newScale = Mathf.Lerp(0.0f, baseScale, t);
        transform.localScale = Vector3.one * newScale;
        
        base.FixedUpdate();
    }
}