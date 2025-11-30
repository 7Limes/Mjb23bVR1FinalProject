using UnityEngine;

[CreateAssetMenu(fileName = "Bounce Factory", menuName = "Scriptable Objects/BounceFactory")]
public class BounceFactory : ProjectileModifier {

    [Tooltip("Determines how quickly the bounciness grows. Higher value = slower, Lower value = faster")]
    [SerializeField] private float initialBounciness = 0.1f;
    [SerializeField] private float growthCoefficient = 0.741f;
    public override void ApplyInitial(GameObject projectile) {
        var dynamicProjectile = projectile.GetComponent<DynamicProjectile>();
        if (dynamicProjectile != null) {
            dynamicProjectile.SetExpireOnCollision(false);

            Collider projCollider = projectile.GetComponent<Collider>();
            
            if (projCollider.material == null) {
                PhysicsMaterial material = new PhysicsMaterial();
                material.bounciness = initialBounciness;
                projCollider.material = material;
            }
            else {
                float bounce = projCollider.material.bounciness;
                float newBounce = growthCoefficient*bounce + (1-growthCoefficient);
                projCollider.material.bounciness = newBounce;
            }
        }
    }
}
