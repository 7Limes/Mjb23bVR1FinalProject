using UnityEngine;

[CreateAssetMenu(fileName = "LifetimeFactory", menuName = "Scriptable Objects/LifetimeFactory")]
public class LifetimeFactory : ProjectileModifier {
    [SerializeField] private float lifetimeAddition = 0.0f;

    public override void ApplyInitial(GameObject projectile) {
        var projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null) {
            float newLifetime = projectileScript.GetLifetime() + lifetimeAddition;
            projectileScript.SetLifetime(newLifetime);

            // Adjust particle durations
            if (projectile.CompareTag("ChangeParticleLifetime")) {
                var particles = projectile.GetComponentsInChildren<ParticleSystem>();
                foreach (var particle in particles) {
                    particle.Stop();
                    var main = particle.main;
                    main.duration = newLifetime;
                    main.startLifetime = newLifetime;
                    particle.Play();
                }
            }
        }
    }
}
