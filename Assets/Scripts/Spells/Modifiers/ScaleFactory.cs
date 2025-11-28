using UnityEngine;

[CreateAssetMenu(fileName = "Scale Factory", menuName = "Scriptable Objects/ScaleFactory")]
public class ScaleFactory : ProjectileModifier {
    [SerializeField] private float scaleMultiplier = 1.0f;

    public override void ApplyInitial(GameObject projectile) {
        projectile.transform.localScale *= scaleMultiplier;
    }
}
