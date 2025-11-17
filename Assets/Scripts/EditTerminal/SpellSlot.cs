using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpellSlot : MonoBehaviour {
    [SerializeField] private Transform attachTransform;

    [SerializeField] private float oscillateSpeed = 1.0f;
    [SerializeField] private float oscillateAmplitude = 0.01f;
    
    private float oscillateOffset = 0.0f;
    private Vector3 basePosition = Vector3.zero;

    XRSocketInteractor socketInteractor;

    GameObject spellCube = null;
    SpellEntry spellEntry = null;

    public void OnAttach() {
        IXRSelectInteractable interactable = socketInteractor.firstInteractableSelected;
        spellCube = (interactable as MonoBehaviour)?.gameObject;
        SpellCube spellCubeScript = spellCube.GetComponent<SpellCube>();
        spellEntry = spellCubeScript.GetSpell();
    }

    public void OnDetach() {
        spellCube = null;
        spellEntry = null;
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

    public void SetOscillateOffset(float offset) {
        oscillateOffset = offset;
    }
    
    void Start() {
        basePosition = transform.position;
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    void Update() {
        float oscillateY = oscillateAmplitude * Mathf.Sin(Time.time * oscillateSpeed + oscillateOffset);
        Vector3 newPosition = basePosition + new Vector3(0, oscillateY, 0);
        transform.position = newPosition;
    }
}
