using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;

/// <summary>
/// Applies a repulsive force to all rigid bodies within a specified radius.
/// The force strength decreases with distance from the center.
/// </summary>
public class ForceSphere : MonoBehaviour {
    [SerializeField] private float forceStrength = 100f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;

    [Tooltip("How the force decreases with distance (1 = linear, 2 = quadratic)")]
    [SerializeField] private float falloffExponent = 2f;

    [Tooltip("If true, force is only applied once per rigidbody")]
    [SerializeField] private bool applyOnce = false;

    [SerializeField] private LayerMask excludedLayers;

    [SerializeField] private bool showGizmo = true;

    [SerializeField] private Color gizmoColor = new Color(1f, 0.5f, 0f, 0.3f);

    private HashSet<Rigidbody> affectedRigidbodies = new HashSet<Rigidbody>();

    private void FixedUpdate() {
        ApplyForceToNearbyRigidbodies();
    }

    private void ApplyForceToNearbyRigidbodies() {
        // Find all colliders within the sphere radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders) {
            Rigidbody rb = col.attachedRigidbody;

            // Skip if no rigidbody or it's kinematic
            if (rb == null || rb.isKinematic)
                continue;

            // Skip if it's this object's rigidbody
            if (rb.transform == transform)
                continue;

            if (excludedLayers.Contains(rb.gameObject.layer))
                continue;

            // Skip if applyOnce is enabled and we've already affected this rigidbody
            if (applyOnce && affectedRigidbodies.Contains(rb))
                continue;

            // Calculate direction from sphere center to rigidbody
            Vector3 direction = rb.position - transform.position;
            float distance = direction.magnitude;

            // Skip if exactly at center to avoid division by zero
            if (distance < 0.001f)
                continue;

            // Normalize direction
            direction.Normalize();

            // Calculate force strength based on distance (inverse falloff)
            float normalizedDistance = Mathf.Clamp01(distance / radius);
            float falloffMultiplier = 1f - Mathf.Pow(normalizedDistance, falloffExponent);
            float actualForce = forceStrength * falloffMultiplier;

            // Apply the force
            rb.AddForce(direction * actualForce, forceMode);

            // Mark this rigidbody as affected if applyOnce is enabled
            if (applyOnce) {
                affectedRigidbodies.Add(rb);
            }
        }
    }

    private void OnDrawGizmos() {
        if (!showGizmo)
            return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);

        // Draw wireframe for better visibility
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}