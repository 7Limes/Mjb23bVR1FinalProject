using UnityEngine;

[CreateAssetMenu(fileName = "ReduceDelayFactory", menuName = "Scriptable Objects/ReduceDelayFactory")]
public class ReduceDelay : SpellFactory {
    [SerializeField] private float castDelay = -0.25f;
    
    public override void AddToGroup(SpellGroup group) {
        group.AddCastDelay(castDelay);
    }
}
