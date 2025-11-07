using UnityEngine;

[CreateAssetMenu(fileName = "FlattenHorizontal", menuName = "Scriptable Objects/FlattenHorizontal")]
public class FlattenHorizontal : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddSpread(new Vector2(0, -360));
    }
}
