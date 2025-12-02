using UnityEngine;

[CreateAssetMenu(fileName = "ConcurrentCastFactory", menuName = "Scriptable Objects/ConcurrentCastFactory")]
public class ConcurrentCastFactory : SpellFactory {
    public override void AddToGroup(SpellGroup group) {
        group.AddConcurrentCasts(1);
        base.AddToGroup(group);
    }
}
