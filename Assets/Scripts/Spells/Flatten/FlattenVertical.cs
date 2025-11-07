using UnityEngine;

[CreateAssetMenu(fileName = "FlattenVertical", menuName = "Scriptable Objects/FlattenVertical")]
public class FlattenVertical : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddSpread(new Vector2(-360, 0));
    }
}
