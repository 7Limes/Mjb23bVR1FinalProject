using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Wand : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float oscillateSpeed = 1.0f;
    [SerializeField] private float oscillateAmplitude = 0.05f;

    [SerializeField] private float holdDistance = 0.05f;  // The maximum distance at which the wand is considered "held"
    [SerializeField] private float castCooldown = 0.5f;
    [SerializeField] private int capacity = 10;

    [SerializeField] private Transform castPosition;

    private GameObject wandModel;
    private Rigidbody rigidBody;
    private IXRSelectInteractor currentInteractor = null;
    private XRInputData inputData = null;

    private float animationTimeOffset = 0.0f;

    private bool isGrabbed = false;  // Whether the wand has been grabbed ("selected")
    private bool isHeld = false;     // Whether the wand is actually in the player's hand
    private bool doIdleAnimation = true;

    private float castTimer = 0.0f;

    private List<SpellEntry> spells = new List<SpellEntry>();
    private List<SpellGroup> groups = new List<SpellGroup>();
    private int groupIndex = 0;

    public void SetWandModel(GameObject model) {
        wandModel = model;
    }

    public void SetIdleAnimation(bool enabled) {
        doIdleAnimation = enabled;
    }

    public void SetCapacity(int newCapacity) {
        capacity = newCapacity;
    }

    public int GetCapacity() {
        return capacity;
    }

    public SpellEntry GetSpell(int index) {
        return spells[index];
    }

    public void SetSpell(SpellEntry spellEntry, int index) {
        spells[index] = spellEntry;
    }
    
    public void OnGrab() {
        isGrabbed = true;
        doIdleAnimation = false;
        currentInteractor = GetComponent<XRGrabInteractable>().firstInteractorSelecting;
        castTimer = 0.5f;
    }

    public void OnRelease() {
        isGrabbed = false;
        isHeld = false;
        doIdleAnimation = true;
        currentInteractor = null;
        wandModel.transform.localPosition = Vector3.zero;
        wandModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
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
            Debug.Log("Index: " + index);
            groups.Add(group);
        }
    }

    public void Cast() {
        if (isHeld && castTimer == 0.0f) {
            if (groups.Count > 0) {
                Debug.Log("Casting group " + groupIndex);
                groups[groupIndex].Cast(castPosition.position, castPosition.rotation);
                groupIndex = (groupIndex + 1) % groups.Count;
            }
            else {
                Debug.Log("Wand is empty.");
            }
            castTimer = castCooldown;
        }
    }

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        inputData = GetComponent<XRInputData>();

        animationTimeOffset = Random.Range(0.0f, 10f);

        // Fill spells list with null to indicate empty
        for (int i = 0; i < capacity; i++) {
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
        if (castTimer > 0) {
            castTimer = Mathf.MoveTowards(castTimer, 0.0f, Time.fixedDeltaTime);
        }
        
        if (isGrabbed) {
            // Update held status
            if (currentInteractor != null) {
                float distance = Vector3.Distance(currentInteractor.transform.position, transform.position);
                isHeld = distance < holdDistance;
            }
        }
    }
}
