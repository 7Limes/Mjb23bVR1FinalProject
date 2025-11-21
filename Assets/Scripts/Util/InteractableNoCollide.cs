using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Disables colliders on interactables if their interactor's root object has a whitelisted tag.
/// </summary>
public class InteractableNoCollide : MonoBehaviour {
    [SerializeField] private List<string> whitelistedTags = new List<string>();

    private IXRSelectInteractable interactable;
    private Collider objectCollider;

    bool IsWhitelisted(Transform obj) {
        foreach (string tag in whitelistedTags) {
            if (obj.CompareTag(tag)) {
                return true;
            }
        }
        return false;
    }

    void AddListeners() {
        objectCollider = GetComponent<Collider>();
        if (objectCollider == null) {
            Debug.LogWarning("InteractableNoCollide: Could not find collider on object. Please add one.");
            return;
        }

        interactable = GetComponent<IXRSelectInteractable>();
        if (interactable == null) {
            Debug.LogWarning("InteractableNoCollide: Could not find interactable component on object. Please add one.");
            return;
        }

        interactable.selectEntered.AddListener((e) => {
            Transform selectorRoot = e.interactorObject.transform.root;
            if (IsWhitelisted(selectorRoot)) {
                objectCollider.enabled = false;
            }
        });

        interactable.selectExited.AddListener((e) => {
            Transform selectorRoot = e.interactorObject.transform.root;
            if (IsWhitelisted(selectorRoot)) {
                objectCollider.enabled = true;
            }
        });
    }

    void Start() {
        AddListeners();
    }
}
