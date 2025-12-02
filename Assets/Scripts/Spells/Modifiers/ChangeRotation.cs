using UnityEngine;

[CreateAssetMenu(fileName = "ChangeRotationFactory", menuName = "Scriptable Objects/ChangeRotationFactory")]
public class ChangeRotationFactory : ProjectileModifier {
    [SerializeField] private float pitchChange = 0.0f;
    [SerializeField] private float yawChange = 0.0f;

    public override void ApplyInitial(GameObject projectile) {
        var dynamicProjectile = projectile.GetComponent<DynamicProjectile>();
        if (dynamicProjectile != null) {
            Quaternion pitchRotation = Quaternion.AngleAxis(pitchChange, Vector3.right);
            Quaternion yawRotation = Quaternion.AngleAxis(yawChange, Vector3.up);
            Quaternion newRotation = projectile.transform.rotation * pitchRotation * yawRotation;
            projectile.transform.rotation = newRotation;

            Rigidbody rb = dynamicProjectile.GetRigidbody();
            rb.linearVelocity = newRotation * Vector3.forward * rb.linearVelocity.magnitude;
        }
    }
}
