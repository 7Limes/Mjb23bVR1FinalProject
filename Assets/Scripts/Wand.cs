using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float oscillateSpeed = 1.0f;
    [SerializeField] private float oscillateAmplitude = 0.05f;

    [SerializeField] private float holdDistance = 0.05f;  // The maximum distance at which the wand is considered "held"
    [SerializeField] private float flickThreshold = 250f;
    [SerializeField] private float castCooldown = 1.0f;
    [SerializeField] private int capacity = 10;

    [SerializeField] private Transform castPosition;

    private GameObject wandModel;
    private Rigidbody rigidBody;
    private IXRSelectInteractor currentInteractor = null;

    private float animationTimeOffset = 0.0f;

    private bool isGrabbed = false;  // Whether the wand has been grabbed ("selected")
    private bool isHeld = false;     // Whether the wand is actually in the player's hand
    private bool doIdleAnimation = true;

    const int ANGULAR_VELOCITY_BUFFER_SIZE = 10;
    private Queue<Vector3> angularVelocityBuffer = new Queue<Vector3>(ANGULAR_VELOCITY_BUFFER_SIZE);
    private Quaternion previousRotation = Quaternion.Euler(0, 0, 0);
    private Vector3 smoothedAngularVelocity = Vector3.zero;

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
        UpdateSpellGroups();
    }
    
    public void OnGrab() {
        isGrabbed = true;
        doIdleAnimation = false;
        currentInteractor = GetComponent<XRGrabInteractable>().firstInteractorSelecting;
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

    void UpdateSpellGroups() {
        groups.Clear();
        int index = 0;
        while (index < spells.Count) {
            var group = new SpellGroup(spells, index);
            group.Build();
            index = group.GetIndex();
            groups.Add(group);
        }
    }

    void Cast() {
        if (groups.Count > 0) {
            groups[groupIndex].Cast(castPosition);
            groupIndex = (groupIndex + 1) % groups.Count;
            castTimer = castCooldown;
        }
        else {
            Debug.Log("Wand is empty.");
        }
    }

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        animationTimeOffset = Random.Range(0.0f, 10f);

        // Fill angular velocity buffer with zeroed vectors
        for (int i = 0; i < ANGULAR_VELOCITY_BUFFER_SIZE; i++) {
            angularVelocityBuffer.Enqueue(Vector3.zero);
        }

        // Fill spells list with null to indicate empty
        for (int i = 0; i < capacity; i++) {
            spells.Add(null);
        }
    }
    
    void UpdateAngularVelocity() {
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
        float angle;
        Vector3 axis;
        deltaRotation.ToAngleAxis(out angle, out axis);

        if (angle > 180) {
            angle -= 360;
        }

        Vector3 angularVelocity = axis * (angle / Time.fixedDeltaTime);
        angularVelocityBuffer.Dequeue();
        angularVelocityBuffer.Enqueue(angularVelocity);
        previousRotation = currentRotation;

        smoothedAngularVelocity = Vector3.zero;
        foreach (Vector3 v in angularVelocityBuffer) {
            smoothedAngularVelocity += v;
        }
        smoothedAngularVelocity /= ANGULAR_VELOCITY_BUFFER_SIZE;
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

            UpdateAngularVelocity();

            // Check for flick
            if (isHeld && smoothedAngularVelocity.magnitude > flickThreshold && castTimer == 0) {
                Cast();
            }
        }
    }
}
