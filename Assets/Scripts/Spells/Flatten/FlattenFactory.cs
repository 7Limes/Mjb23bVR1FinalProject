using UnityEngine;

[CreateAssetMenu(fileName = "Flatten Factory", menuName = "Scriptable Objects/FlattenFactory")]
public class FlattenFactory : SpellFactory {
    [SerializeField] private Vector2 addSpread = Vector2.zero;
    public override void AddToGroup(SpellGroup group) {
        group.AddSpread(addSpread);
        base.AddToGroup(group);
    }
}
