using UnityEngine;

[CreateAssetMenu(fileName = "LinearArcFactory", menuName = "Scriptable Objects/LinearArcFactory")]
public class LinearArcFactory : ProjectileModifier {
    Quaternion AlignQuaternion(Quaternion rotation) {
        Vector3 forward = rotation * Vector3.forward;
        Vector3 snappedForward;

        if (Mathf.Abs(forward.x) > Mathf.Abs(forward.y) && Mathf.Abs(forward.x) > Mathf.Abs(forward.z)) {
            snappedForward = new Vector3(Mathf.Sign(forward.x), 0, 0);
        }
        else if (Mathf.Abs(forward.y) > Mathf.Abs(forward.z)) {
            snappedForward = new Vector3(0, Mathf.Sign(forward.y), 0);
        }
        else {
            snappedForward = new Vector3(0, 0, Mathf.Sign(forward.z));
        }

        return Quaternion.LookRotation(snappedForward);
    }

    public override void ApplyInitial(GameObject projectile) {
        var dynamicProjectile = projectile.GetComponent<DynamicProjectile>();
        if (dynamicProjectile != null) {
            Quaternion alignedRotation = AlignQuaternion(projectile.transform.rotation);
            projectile.transform.rotation = alignedRotation;
            Rigidbody rb = dynamicProjectile.GetRigidbody();
            rb.linearVelocity = alignedRotation * Vector3.forward * rb.linearVelocity.magnitude;
        }
    }
}
