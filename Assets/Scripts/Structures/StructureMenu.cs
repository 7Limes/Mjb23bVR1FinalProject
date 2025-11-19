using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StructureMenu : MonoBehaviour {
    [SerializeField] private GameObject structureListContent;
    [SerializeField] private GameObject structureButtonPrefab;

    [SerializeField] private Transform structureSpawnPoint;

    [SerializeField] private Vector2 structureListOffset = Vector2.zero;
    [SerializeField] private float structureListSpacing = 0.05f;
    
    [SerializeField] private List<StructureData> structures = new List<StructureData>();

    public void ClearStructure() {
        foreach (Transform child in structureSpawnPoint.transform) {
            Destroy(child.gameObject);
        }
    }

    void InstantiateStructure(GameObject prefab) {
        ClearStructure();
        Instantiate(prefab, structureSpawnPoint);
    }

    void Start() {
        Vector3 translationVector = new Vector3(structureListOffset.x, structureListOffset.y, 0);
        foreach (var structure in structures) {
            GameObject structureButton = Instantiate(structureButtonPrefab, structureListContent.transform);
            structureButton.transform.localPosition += translationVector;
            translationVector.y -= structureListSpacing;

            var buttonText = structureButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.SetText(structure.structureName);
               
            // Bind events to buttons
            var buttonScript = structureButton.GetComponent<Button>();
            buttonScript.onClick.AddListener(() => {
                InstantiateStructure(structure.prefab);
            });
        }
    }
}