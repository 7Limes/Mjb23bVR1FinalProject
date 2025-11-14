using UnityEngine;

[CreateAssetMenu(fileName = "MagicWallFactory", menuName = "Scriptable Objects/MagicWallFactory")]
public class MagicWallFactory : ProjectileFactory {
    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        Vector3 newAngles = new Vector3(0, castRotation.eulerAngles.y, 0);
        castRotation.eulerAngles = newAngles;
        return base.Cast(castPosition, castRotation);
    }
}
