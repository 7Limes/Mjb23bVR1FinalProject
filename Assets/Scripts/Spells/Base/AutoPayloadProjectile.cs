using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Auto Payload Projectile", menuName = "Scriptable Objects/AutoPayloadProjectile")]
public class AutoPayloadProjectileFactory : PayloadProjectileFactory {
    [SerializeField] private List<ProjectileFactory> autoPayloadSpells;

    public override void AddToGroup(SpellGroup group) {

        // Add hit effect pseudo-spell to the payload
        payloadGroup = new SpellGroup();
        foreach (var projectile in autoPayloadSpells) {
            payloadGroup.AddProjectile(projectile);
        }

        base.AddToGroup(group);
    }   

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        return obj;
    }
}
