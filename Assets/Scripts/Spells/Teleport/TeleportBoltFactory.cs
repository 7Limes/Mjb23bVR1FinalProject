using UnityEngine;

[CreateAssetMenu(fileName = "TeleportBoltFactory", menuName = "Scriptable Objects/TeleportBoltFactory")]
public class TeleportBoltFactory : LifetimeProjectileFactory {
    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        return base.Cast(castPosition, castRotation);
    }
}
