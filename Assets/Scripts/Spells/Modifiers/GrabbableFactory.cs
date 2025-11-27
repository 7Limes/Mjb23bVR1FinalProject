using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[CreateAssetMenu(fileName = "GrabbableFactory", menuName = "Scriptable Objects/GrabbableFactory")]
public class GrabbableFactory : ProjectileModifier {
    public override void ApplyInitial(GameObject projectile) {
        if (projectile.GetComponent<XRGrabInteractable>() == null) {   
            var interactable = projectile.AddComponent<XRGrabInteractable>();
        }
    }
}
