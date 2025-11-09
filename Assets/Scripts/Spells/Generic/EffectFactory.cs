using UnityEngine;

[CreateAssetMenu(fileName = "EffectFactory", menuName = "Scriptable Objects/EffectFactory")]
public class EffectFactory : ProjectileFactory {
    [SerializeField] private GameObject effectPrefab;

    public void SetEffectPrefab(GameObject prefab) {
        effectPrefab = prefab;
    }

    public override GameObject Cast(Vector3 castPosition, Quaternion castRotation) {
        return Instantiate(effectPrefab, castPosition, castRotation);
    }
}

