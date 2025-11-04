using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections.Generic;

public class EditTerminal : MonoBehaviour {

    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private Transform slotsBasePosition;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private float spellSlotSpacing = 0.5f;

    GameObject attachedWandObject = null;
    Wand attachedWand = null;

    List<GameObject> slotObjects = new List<GameObject>();
    List<SpellEntry> spells = new List<SpellEntry>();

    public void OnWandSocketAttach() {
        if (socketInteractor.hasSelection) {
            IXRSelectInteractable interactable = socketInteractor.firstInteractableSelected;
            attachedWandObject = (interactable as MonoBehaviour)?.gameObject;
            attachedWand = attachedWandObject.GetComponent<Wand>();
            attachedWand.SetIdleAnimation(true);
            CreateSlots();
        }
    }

    public void OnWandSocketDetach() {
        attachedWand.SetIdleAnimation(false);
        ClearSlots();

        attachedWandObject = null;
        attachedWand = null;
    }

    private void CreateSlots() {
        int wandCapacity = attachedWand.GetCapacity();
        for (int i = 0; i < wandCapacity; i++) {
            GameObject slotObject = Instantiate(spellSlotPrefab, slotsBasePosition);
            Vector3 slotPosition = slotObject.transform.localPosition;
            slotPosition.x += i * spellSlotSpacing;
            slotObject.transform.localPosition = slotPosition;
            slotObjects.Add(slotObject);
        }
    }
    

    private void ClearSlots() {
        foreach (GameObject slotObject in slotObjects) {
            Destroy(slotObject);
        }
    }
}
