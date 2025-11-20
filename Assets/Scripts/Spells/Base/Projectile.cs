using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
    protected float lifetime;
    private List<ProjectileModifier> modifiers;

    protected virtual void OnExpire() {
        Destroy(gameObject);
    }

    public void Initialize(float lifetime) {
        this.lifetime = lifetime;
    }

    public void SetLifetime(float newLifetime) {
        lifetime = newLifetime;
    }

    public float GetLifetime() {
        return lifetime;
    }

    public void SetModifiers(List<ProjectileModifier> mods) {
        modifiers = mods;
    }

    protected virtual void FixedUpdate() {
        if (lifetime != -1) {
            lifetime = Mathf.MoveTowards(lifetime, 0.0f, Time.fixedDeltaTime);
            if (lifetime == 0) {
                OnExpire();
            }
        }

        if (modifiers != null) {
            foreach (ProjectileModifier mod in modifiers) {
                mod.ApplyContinuous(gameObject);
            }
        }
    }
}
