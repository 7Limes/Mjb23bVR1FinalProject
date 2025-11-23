using UnityEngine;
[CreateAssetMenu(fileName = "Multicast Factory", menuName = "Scriptable Objects/MulticastFactory")]

public class MulticastFactory : SpellFactory {
    [SerializeField] protected int multicastAmount = 2;
    public override void AddToGroup(SpellGroup group) {
        group.AddMulticast(multicastAmount);
        group.DecrementCastable();
    }
}