using UnityEngine;

[CreateAssetMenu(fileName = "StructureData", menuName = "Scriptable Objects/StructureData")]
public class StructureData : ScriptableObject {
    public string structureName;
    public GameObject prefab;
}
