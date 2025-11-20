using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EditTerminal : MonoBehaviour {
    [SerializeField] private Transform slotsBasePosition;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private float spellSlotSpacing = 0.5f;
    [SerializeField] private readonly int slotsPerRow = 8;


    SpellCubeCreator spellCubeCreator;
    Wand attachedWand = null;

    List<GameObject> slotObjects = new List<GameObject>();

    public void SetAttachedWand(Wand wand) {
        attachedWand = wand;
    }

    void Start() {
        spellCubeCreator = GetComponent<SpellCubeCreator>();
    }

    public void UpdateWandSpells() {
        for (int i = 0; i < slotObjects.Count; i++) {
            GameObject slotObject = slotObjects[i];
            SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
            SpellEntry spell = spellSlotScript.GetSpell();
            attachedWand.SetSpell(spell, i);
        }
        attachedWand.UpdateSpellGroups();
    }

    public void CreateSlots() {
        int wandCapacity = attachedWand.capacity;
        for (int i = 0; i < wandCapacity; i++) {
            GameObject slotObject = Instantiate(spellSlotPrefab, slotsBasePosition);

            SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
            spellSlotScript.SetOscillateOffset(i * 0.5f);

            // Move slot
            Vector3 slotPosition = slotObject.transform.localPosition;
            slotPosition.x += i % slotsPerRow * spellSlotSpacing;
            slotPosition.y -= i / slotsPerRow * spellSlotSpacing;

            if (wandCapacity <= slotsPerRow) {
                // Shift down if there's only 1 row
                slotPosition.y -= spellSlotSpacing / 2;
            }

            slotObject.transform.localPosition = slotPosition;

            // Create spell cube
            SpellEntry spellEntry = attachedWand.GetSpell(i);
            if (spellEntry != null) {
                spellCubeCreator.CreateSpellCube(spellEntry, spellSlotScript.GetAttachTransform());
            }

            slotObjects.Add(slotObject);
        }
    }

    public void ClearSlots() {
        foreach (GameObject slotObject in slotObjects) {
            SpellSlot spellSlotScript = slotObject.GetComponent<SpellSlot>();
            GameObject spellCube = spellSlotScript.GetSpellCube();
            Destroy(slotObject);
            Destroy(spellCube);
        }
        slotObjects.Clear();
    }
}
