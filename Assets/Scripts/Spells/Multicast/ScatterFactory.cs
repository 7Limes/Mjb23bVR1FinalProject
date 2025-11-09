using UnityEngine;
[CreateAssetMenu(fileName = "Scatter Factory", menuName = "Scriptable Objects/ScatterFactory")]

public class ScatterFactory : MulticastFactory {
    [SerializeField] protected float spreadDegrees = 10;
    public override void AddToGroup(SpellGroup group) {
        group.AddMulticast(multicastAmount);
        group.AddSpread(spreadDegrees);
        group.DecrementCastable();
    }
}