using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class StructureMenu : MonoBehaviour {
    [SerializeField] private GameObject structureListContent;
    [SerializeField] private GameObject structureButtonPrefab;

    [SerializeField] private Vector2 structureListOffset = Vector2.zero;
    [SerializeField] private float structureListSpacing = 0.05f;
    
    [SerializeField] private List<StructureData> structures = new List<StructureData>();

    void Start() {
        Vector3 translationVector = new Vector3(structureListOffset.x, structureListOffset.y, 0);
        foreach (var structure in structures) {
            GameObject structureButton = Instantiate(structureButtonPrefab, structureListContent.transform);
            structureButton.transform.localPosition += translationVector;
            translationVector.y -= structureListSpacing;

            var buttonText = structureButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.SetText(structure.structureName);
        }
    }
}