using UnityEngine;

[CreateAssetMenu(fileName = "CapsuleTriggerFactory", menuName = "Scriptable Objects/CapsuleTriggerFactory")]
public class CapsuleTriggerFactory : PayloadProjectileFactory {
    [SerializeField] private float breakForce = 50f;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        var script = obj.GetComponent<CapsuleTrigger>();
        script.SetBreakForce(breakForce);

        return obj;
    }
}

