using UnityEngine;

[CreateAssetMenu(fileName = "Spell Factory", menuName = "Scriptable Objects/SpellFactory")]

public class SpellFactory : ScriptableObject {
    [Header("General Spell Settings")]
    [Tooltip("The cast delay to add to the group in seconds.")]
    [SerializeField] private float castDelayChange = 0.0f;

    [Tooltip("The spread to add to the group in degrees.")]
    [SerializeField] private float spreadChange = 0.0f;

    protected void ApplyGeneralStatChanges(SpellGroup group) {
        group.AddCastDelay(castDelayChange);
        group.AddSpread(spreadChange);
    }

    virtual public void AddToGroup(SpellGroup group) {
        ApplyGeneralStatChanges(group);
    }
}

