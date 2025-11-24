using UnityEngine;

[CreateAssetMenu(fileName = "Homing", menuName = "Scriptable Objects/Homing")]
public class Homing : ProjectileModifier {
    [SerializeField] private string targetTag = "Enemy";
    
    [SerializeField] private float homingRadius = 10f;
    [SerializeField] private float homingForce = 1.5f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float damping = 0.5f;

    public override void ApplyInitial(GameObject projectile) {
        if (projectile.GetComponent<TagFinder>() == null) {
            var enemyFinder = projectile.AddComponent<TagFinder>();
            enemyFinder.SetDetectionRadius(homingRadius);
            enemyFinder.SetTargetTag(targetTag);
        }
    }

    public override void ApplyContinuous(GameObject projectile) {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null) {
            return;
        }

        var enemyFinder = projectile.GetComponent<TagFinder>();
        GameObject nearestEnemy = enemyFinder.GetNearestObject();
        if (nearestEnemy != null) {
            Transform enemyTransform = nearestEnemy.transform;

            Vector3 direction = (enemyTransform.position - projectile.transform.position).normalized;
            rb.AddForce(direction * homingForce);

            Vector3 perpVelocity = rb.linearVelocity - Vector3.Project(rb.linearVelocity, direction);
            rb.linearVelocity -= perpVelocity * damping;


            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);

            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
