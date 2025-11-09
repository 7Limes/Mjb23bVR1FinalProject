using UnityEngine;

[CreateAssetMenu(fileName = "Spark Bolt Factory", menuName = "Scriptable Objects/SparkBoltFactory")]
public class SparkBoltFactory : PayloadProjectileFactory {
    [SerializeField] private GameObject hitEffectPrefab;

    public override void AddToGroup(SpellGroup group) {

        // Add hit effect pseudo-spell to the payload
        payloadGroup = new SpellGroup();
        EffectFactory effectSpell = CreateInstance<EffectFactory>();
        effectSpell.SetEffectPrefab(hitEffectPrefab);
        payloadGroup.AddProjectile(effectSpell);

        base.AddToGroup(group);
    }   

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        return obj;
    }
}
