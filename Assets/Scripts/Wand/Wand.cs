using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using System;

public class Wand : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float oscillateSpeed = 1.0f;
    [SerializeField] private float oscillateAmplitude = 0.05f;

    [SerializeField] private Transform castPosition;

    public float castDelay = 0.5f;
    public float spread = 0.0f;
    public int capacity = 10;

    private GameObject wandModel;
    private Rigidbody rigidBody;
    private IXRSelectInteractor currentInteractor = null;

    private float animationTimeOffset = 0.0f;

    private bool isGrabbed = false;  // Whether the wand has been grabbed ("selected")
    private bool triggerPressed = false;
    private bool prevTriggerPressed = false; // The state of the trigger on the last tick
    private bool doIdleAnimation = true;

    private float castDelayTimer = 0.0f;

    private List<SpellEntry> spells = new List<SpellEntry>();
    private List<SpellGroup> groups = new List<SpellGroup>();
    private int groupIndex = 0;

    private GlobalSettings settings;

    public void SetWandModel(GameObject model) {
        wandModel = model;
    }

    public void SetIdleAnimation(bool enabled) {
        doIdleAnimation = enabled;
    }
 
    public SpellEntry GetSpell(int index) {
        return spells[index];
    }

    public void SetSpell(SpellEntry spellEntry, int index) {
        spells[index] = spellEntry;
    }

    public void SetSpells(List<SpellEntry> newSpells) {
        spells.Clear();
        foreach (SpellEntry entry in newSpells) {
            spells.Add(entry);
        }

        capacity = Math.Max(capacity, newSpells.Count);

        // Pad with nulls
        while (spells.Count < capacity) {
            spells.Add(null);
        }
    }
    
    public void OnGrab() {
        isGrabbed = true;
        doIdleAnimation = false;
        currentInteractor = GetComponent<XRGrabInteractable>().firstInteractorSelecting;
        castDelayTimer = castDelay;
    }

    public void OnRelease() {
        isGrabbed = false;
        triggerPressed = false;
        doIdleAnimation = true;
        currentInteractor = null;
        wandModel.transform.localPosition = Vector3.zero;
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnTriggerPress() {
        triggerPressed = true;
    }

    public void OnTriggerRelease() {
        triggerPressed = false;
    }

    public void UpdateSpellGroups() {
        groups.Clear();
        groupIndex = 0;

        int index = 0;
        while (index < spells.Count) {
            var group = new SpellGroup(spells, index);
            group.Build();
            if (group.IsEmpty()) {
                break;
            }
            
            index = group.GetIndex();
            groups.Add(group);
        }
    }

    void HapticImpulse(float amplitude, float duration) {
        var hapticPlayer = currentInteractor.transform.GetComponentInParent<HapticImpulsePlayer>();
        hapticPlayer.SendHapticImpulse(amplitude, duration);
    }

    public void Cast() {
        if (isGrabbed && castDelayTimer == 0.0f) {
            if (groups.Count > 0) {
                SpellGroup currentGroup = groups[groupIndex];
                Vector2 spreadVector = new Vector2(spread, spread);
                currentGroup.Cast(castPosition.position, castPosition.rotation, spreadVector);
                castDelayTimer = castDelay + currentGroup.GetCastDelay();
                castDelayTimer = Mathf.Clamp(castDelayTimer, 0, 999);
                groupIndex = (groupIndex + 1) % groups.Count;
                HapticImpulse(0.75f, 0.15f);
            }
            else {
                castDelayTimer = castDelay;
                HapticImpulse(1.0f, 0.5f);
            }
        }
    }

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        
        settings = Resources.Load<GlobalSettings>("GlobalSettings");

        animationTimeOffset = UnityEngine.Random.Range(0.0f, 10f);

        // Fill spells list with null to indicate empty
        while (spells.Count < capacity) {
            spells.Add(null);
        }
    }

    void Update() {
        rigidBody.freezeRotation = !isGrabbed;
        if (doIdleAnimation) {
            float time = Time.fixedTime + animationTimeOffset;
            Vector3 rotateVector = new Vector3(0, time * rotateSpeed, 0);
            wandModel.transform.localEulerAngles = rotateVector;

            float oscillateY = oscillateAmplitude * Mathf.Sin(time * oscillateSpeed);
            Vector3 oscillatePosition = new Vector3(0, oscillateY, 0);
            wandModel.transform.localPosition = oscillatePosition;
        }
    }

    void FixedUpdate() {
        if (castDelayTimer > 0) {
            castDelayTimer = Mathf.MoveTowards(castDelayTimer, 0.0f, Time.fixedDeltaTime);
        }

        if (settings.autoCastEnabled) {
            if (triggerPressed) {
                Cast();
            }
        }
        else {
            if (!prevTriggerPressed && triggerPressed) {
                Cast();
            }
            prevTriggerPressed = triggerPressed;
        }

    }
}
