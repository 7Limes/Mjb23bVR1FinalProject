using UnityEngine;

abstract public class SpellFactory : ScriptableObject {
    abstract public void AddToGroup(SpellGroup group);
}

