using UnityEngine;
[CreateAssetMenu(fileName = "Double Factory", menuName = "Scriptable Objects/DoubleFactory")]

public class DoubleFactory : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddMulticast(2);
        group.DecrementCastable();
    }
}