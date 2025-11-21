using UnityEngine;

[CreateAssetMenu(fileName = "GravityFactory", menuName = "Scriptable Objects/GravityFactory")]
public class GravityFactory : ProjectileModifier {
    [SerializeField] private float gravityMultiplier = 0.0f;

    public override void ApplyInitial(GameObject projectile) {
        var dynamicProjectile = projectile.GetComponent<DynamicProjectile>();
        if (dynamicProjectile != null) {
            float newGravity = dynamicProjectile.GetGravity() * gravityMultiplier;
            dynamicProjectile.SetGravity(newGravity);
        }
    }
}
