using UnityEngine;

[CreateAssetMenu(fileName = "MagicWallFactory", menuName = "Scriptable Objects/MagicWallFactory")]
public class MagicWallFactory : DynamicProjectileFactory {
    [SerializeField] private Vector3 fixedRotation = new Vector3(-1, -1, -1);
    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        Vector3 euler = castRotation.eulerAngles;
        Vector3 newAngles = new Vector3(
            fixedRotation.x != -1 ? fixedRotation.x : euler.x,
            fixedRotation.y != -1 ? fixedRotation.y : euler.y,
            fixedRotation.z != -1 ? fixedRotation.z : euler.z
        );
        castRotation.eulerAngles = newAngles;
        return base.Cast(castPosition, castRotation);
    }
}
