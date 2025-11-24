using UnityEngine;

[CreateAssetMenu(fileName = "LockRotationFactory", menuName = "Scriptable Objects/LockRotationFactory")]
public class LockRotationFactory : ProjectileModifier {
    private Quaternion baseRotation;

    public override void ApplyInitial(GameObject projectile) {
        baseRotation = projectile.transform.rotation;
    }

    public override void ApplyContinuous(GameObject projectile) {
        projectile.transform.rotation = baseRotation;
    }
}
