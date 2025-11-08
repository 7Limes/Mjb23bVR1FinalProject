using UnityEngine;

[CreateAssetMenu(fileName = "EffectFactory", menuName = "Scriptable Objects/EffectFactory")]
public class EffectFactory : ProjectileFactory {
    [SerializeField] private GameObject effectPrefab;

    public void SetEffectPrefab(GameObject prefab) {
        effectPrefab = prefab;
    }

    public override void AddToGroup(SpellGroup group) {
        return;
    }

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        GameObject obj = Instantiate(effectPrefab, castPosition, castRotation);
        
        obj.AddComponent<Effect>();

        return obj;
    }
}

