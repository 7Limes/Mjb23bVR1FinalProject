using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "Scriptable Objects/SpeedUp")]
public class SpeedUp : Modifier {
    [SerializeField] private float speedMultiplier = 1.5f;

    public override void AddToGroup(SpellGroup group) {
        group.AddModifier(this);
    }

    public override void Apply(GameObject projectile) {
        var dynamicProjectile = projectile.GetComponent<DynamicProjectile>();
        if (dynamicProjectile != null) {
            dynamicProjectile.GetRigidbody().linearVelocity *= speedMultiplier;
        }
    }
}
