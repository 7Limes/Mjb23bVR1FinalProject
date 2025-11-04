using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Spell Entry", menuName = "Scriptable Objects/SpellEntry")]
public class SpellEntry : ScriptableObject {
    public string spellID;
    public Material material;
    public UnityEvent onUse;

    public void Use() {
        onUse?.Invoke();
    }
}