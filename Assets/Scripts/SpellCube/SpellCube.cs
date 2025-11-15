using TMPro;
using UnityEngine;

public class SpellCube : MonoBehaviour {
    [SerializeField] private GameObject iconMesh;
    [SerializeField] private GameObject cubeMesh;
    [SerializeField] private float iconOscillateSpeed = 1.0f;
    [SerializeField] private float iconOscillateAmplitude = 0.001f;

    [SerializeField] private Transform spellInfoTarget;

    [SerializeField] private TextMeshProUGUI spellNameText;
    [SerializeField] private TextMeshProUGUI spellDescriptionText;

    private Camera playerCamera;

    private SpellEntry spellEntry = null;
    private Rigidbody rb;

    private bool isSuspended = true;

    public SpellEntry GetSpell() {
        return spellEntry;
    }

    public void SetSpell(SpellEntry entry, Material cubeMaterial) {
        spellEntry = entry;
        iconMesh.GetComponent<MeshRenderer>().material = spellEntry.iconMaterial;
        cubeMesh.GetComponent<MeshRenderer>().material = cubeMaterial;

        string spellName = spellEntry?.spellName ?? "Unnamed Spell";
        string spellDescription = spellEntry?.spellDescription ?? "No description provided";
        spellNameText.SetText(spellName);
        spellDescriptionText.SetText(spellDescription);
    }

    public void SetSuspended(bool isEnabled) {
        isSuspended = isEnabled;
    }

    void Start() {
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = isSuspended;

        // Apply random rotation
        transform.Rotate(new Vector3(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360)
        ));
    }

    void Update() {
        float oscillateY = iconOscillateAmplitude * Mathf.Sin(Time.time * iconOscillateSpeed);
        Vector3 worldUpOffset = Vector3.up * oscillateY;
        Vector3 localOffset = iconMesh.transform.parent.InverseTransformDirection(worldUpOffset);
        iconMesh.transform.localPosition = localOffset;
        iconMesh.transform.rotation = playerCamera.transform.rotation;

        spellInfoTarget.LookAt(playerCamera.transform);
        spellInfoTarget.Rotate(0, 180, 0);

        rb.isKinematic = isSuspended;
        if (isSuspended) {
            Vector3 rotateAngles = new Vector3(0.05f, 0.07f, 0.09f);
            transform.Rotate(rotateAngles);
        }
    }
}
