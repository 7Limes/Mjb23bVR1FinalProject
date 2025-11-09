using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections.Generic;
using TMPro;

public class EditTerminal : MonoBehaviour {

    [SerializeField] private XRSocketInteractor wandSocketInteractor;
    [SerializeField] private TextMeshProUGUI capacityLabel;
    [SerializeField] private TextMeshProUGUI castDelayLabel;
    [SerializeField] private Transform slotsBasePosition;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private float spellSlotSpacing = 0.5f;


    SpellCubeCreator spellCubeCreator;

    GameObject attachedWandObject = null;
    Wand attachedWand = null;

    List<GameObject> slotObjects = new List<GameObject>();

    public void OnWandSocketAttach() {
        if (wandSocketInteractor.hasSelection) {
            IXRSelectInteractable interactable = wandSocketInteractor.firstInteractableSelected;
            attachedWandObject = (interactable as MonoBehaviour)?.gameObject;
            attachedWand = attachedWandObject.GetComponent<Wand>();
            attachedWand.SetIdleAnimation(true);
            CreateSlots();
            UpdateLabels();
        }
    }

    public void OnWandSocketDetach() {
        UpdateWandSpells();
        ClearSlots();

        attachedWand.SetIdleAnimation(false);
        attachedWandObject = null;
        attachedWand = null;
    }

    private void UpdateLabels() {
        capacityLabel.SetText($"Capacity: {attachedWand.GetCapacity()}");
        castDelayLabel.SetText($"Cast Delay: {attachedWand.GetCastDelay():0.00}s");
    }

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
    }

    private void UpdateWandSpells() {
        for (int i = 0; i < slotObjects.Count; i++) {
            GameObject slotObject = slotObjects[i];
            SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
            SpellEntry spell = spellSlotScript.GetSpell();
            attachedWand.SetSpell(spell, i);
        }
        attachedWand.UpdateSpellGroups();
    }

    private void CreateSlots() {
        int wandCapacity = attachedWand.GetCapacity();
        for (int i = 0; i < wandCapacity; i++) {
            GameObject slotObject = Instantiate(spellSlotPrefab, slotsBasePosition);

            // Move slot
            Vector3 slotPosition = slotObject.transform.localPosition;
            slotPosition.x += i * spellSlotSpacing;
            slotObject.transform.localPosition = slotPosition;

            // Create spell cube
            SpellEntry spellEntry = attachedWand.GetSpell(i);
            if (spellEntry != null) {
                SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
                spellCubeCreator.CreateSpellCube(spellEntry.spellID, spellSlotScript.GetAttachTransform());
            }

            slotObjects.Add(slotObject);
        }
    }

    private void ClearSlots() {
        foreach (GameObject slotObject in slotObjects) {
            SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
            GameObject spellCube = spellSlotScript.GetSpellCube();
            Destroy(slotObject);
            Destroy(spellCube);
        }
        slotObjects.Clear();
    }
}
