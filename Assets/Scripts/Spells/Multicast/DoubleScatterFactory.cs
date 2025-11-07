using UnityEngine;
[CreateAssetMenu(fileName = "Double Scatter Factory", menuName = "Scriptable Objects/DoubleScatterFactory")]

public class DoubleScatterFactory : SpellFactory {
    [SerializeField] private float spreadDegrees = 10;
    public override void AddToGroup(SpellGroup group) {
        group.AddMulticast(2);
        group.AddSpread(spreadDegrees);
        group.DecrementCastable();
    }
}