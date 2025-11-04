using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpellSlot : MonoBehaviour {
    [SerializeField] private Transform attachTransform;

    XRSocketInteractor socketInteractor;

    GameObject spellCube = null;
    SpellEntry spellEntry = null;

    public void OnAttach() {
        IXRSelectInteractable interactable = socketInteractor.firstInteractableSelected;
        spellCube = (interactable as MonoBehaviour)?.gameObject;
        SpellCube spellCubeScript = spellCube.GetComponent<SpellCube>();
        spellEntry = spellCubeScript.GetSpell();
        Debug.Log($"Attached spell {spellEntry.spellID}");
    }

    public SpellEntry GetSpell() {
        return spellEntry;
    }

    public GameObject GetSpellCube() {
        return spellCube;
    }

    public Transform GetAttachTransform() {
        return attachTransform;
    }
    
    void Start() {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }
}
