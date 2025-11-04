using UnityEngine;
using UnityEngine.Events;

public class SpellCube : MonoBehaviour {
    [SerializeField] private GameObject iconQuad = null;
    [SerializeField] private float iconOscillateSpeed = 1.0f;
    [SerializeField] private float iconOscillateAmplitude = 0.001f;

    private Camera playerCamera;

    private SpellEntry spellEntry = null;

    public SpellEntry GetSpell() {
        return spellEntry;
    }

    public void SetSpell(SpellEntry entry) {
        spellEntry = entry;
        iconQuad.GetComponent<MeshRenderer>().material = spellEntry.material;
    }

    public UnityEvent GetSpellCastEvent() {
        return spellEntry.onCast;
    }

    void Start() {
        playerCamera = Camera.main;
    }

    void Update() {
        float oscillateY = iconOscillateAmplitude * Mathf.Sin(Time.time * iconOscillateSpeed);
        Vector3 iconOscillateVector = new Vector3(0, oscillateY, 0);
        iconQuad.transform.localPosition = iconOscillateVector;
        iconQuad.transform.rotation = playerCamera.transform.rotation;
    }
}
