using UnityEngine;

[CreateAssetMenu(fileName = "LifetimeFactory", menuName = "Scriptable Objects/LifetimeFactory")]
public class LifetimeFactory : ProjectileModifier {
    [SerializeField] private float lifetimeAddition = 0.0f;

    public override void ApplyInitial(GameObject projectile) {
        var projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null) {
            float newLifetime = projectileScript.GetLifetime() + lifetimeAddition;
            projectileScript.SetLifetime(newLifetime);
        }
    }
}
