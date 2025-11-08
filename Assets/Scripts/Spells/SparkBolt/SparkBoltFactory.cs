using UnityEngine;

[CreateAssetMenu(fileName = "Spark Bolt Factory", menuName = "Scriptable Objects/SparkBoltFactory")]
public class SparkBoltFactory : LifetimeProjectileFactory {
    [SerializeField] private GameObject hitEffectPrefab;

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = base.Cast(castPosition, castRotation);

        SpellGroup group = new SpellGroup();
        EffectFactory effectSpell = CreateInstance<EffectFactory>();
        effectSpell.SetEffectPrefab(hitEffectPrefab);
        group.AddProjectile(effectSpell);

        SparkBolt script = obj.GetComponent<SparkBolt>();
        script.payloadGroup = group;

        return obj;
    }
}
