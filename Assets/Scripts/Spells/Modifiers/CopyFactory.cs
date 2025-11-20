using UnityEngine;

[CreateAssetMenu(fileName = "CopyFactory", menuName = "Scriptable Objects/CopyFactory")]
public class CopyFactory : SpellFactory {
    [SerializeField] private int copyAddition = 2;

    public override void AddToGroup(SpellGroup group) {
        group.AddCopies(copyAddition);
    }
}
