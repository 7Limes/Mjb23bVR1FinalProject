using UnityEngine;

[CreateAssetMenu(fileName = "MagicPlatformFactory", menuName = "Scriptable Objects/MagicPlatformFactory")]
public class MagicPlatformFactory : ProjectileFactory {
    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        Vector3 newAngles = new Vector3(90, castRotation.eulerAngles.y, 0);
        castRotation.eulerAngles = newAngles;
        return base.Cast(castPosition, castRotation);
    }
}
