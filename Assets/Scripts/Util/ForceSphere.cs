using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;

public class ForceSphere : MonoBehaviour {
    [SerializeField] private SphereCollider sphereCollider;

    [SerializeField] private float forceStrength = 100f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;
    [SerializeField] private float duration = 0.5f;

    [Tooltip("How the force decreases with distance (1 = linear, 2 = quadratic)")]
    [SerializeField] private float falloffExponent = 2f;

    [Tooltip("If true, force is only applied once per rigidbody")]
    [SerializeField] private bool applyOnce = false;

    [SerializeField] private LayerMask excludedLayers;

    private HashSet<Rigidbody> affectedRigidbodies = new HashSet<Rigidbody>();
    private float deactivateTimer = 0.0f;

    private void Awake() {
        if (sphereCollider == null) {
            Debug.LogError("ForceSphere: Could not find SphereCollider component");
        }
    }

    private void FixedUpdate() {
        ApplyForceToNearbyRigidbodies();

        deactivateTimer = Mathf.MoveTowards(deactivateTimer, duration, Time.fixedDeltaTime);
        if (deactivateTimer == duration) {
            enabled = false;
        }
    }

    private void ApplyForceToNearbyRigidbodies() {
        if (sphereCollider == null)
            return;

        // Get radius from the SphereCollider, accounting for scale
        float radius = sphereCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        Vector3 center = transform.TransformPoint(sphereCollider.center);

        // Find all colliders within the sphere radius
        Collider[] colliders = Physics.OverlapSphere(center, radius);

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
            Vector3 direction = rb.position - center;
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
}