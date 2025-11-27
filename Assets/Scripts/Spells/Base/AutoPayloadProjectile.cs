using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Auto Payload Projectile", menuName = "Scriptable Objects/AutoPayloadProjectile")]
public class AutoPayloadProjectileFactory : PayloadProjectileFactory {
    [Tooltip("Spells that will be automatically attached to the payload of this projectile.")]
    [SerializeField] private List<ProjectileFactory> autoPayloadSpells;

    public override void AddToGroup(SpellGroup group) {
        payloadGroup = new SpellGroup();
        foreach (var projectile in autoPayloadSpells) {
            payloadGroup.AddProjectile(projectile);
        }

        base.AddToGroup(group);
    }
}
