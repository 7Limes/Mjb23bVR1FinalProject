using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WandSocket : MonoBehaviour {
    [SerializeField] private XRSocketInteractor wandSocketInteractor;
    [SerializeField] private EditTerminal editTerminal;

    [SerializeField] private TextMeshProUGUI capacityLabel;
    [SerializeField] private TextMeshProUGUI castDelayLabel;
    [SerializeField] private TextMeshProUGUI spreadLabel;

    [SerializeField] private List<GameObject> animatedToruses = new List<GameObject>();
    [SerializeField] private float torusRotateSpeed = 0.1f;

    private bool wandIsAttached = false;

    private void UpdateLabels(Wand attachedWand) {
        capacityLabel.SetText($"Capacity: {attachedWand.capacity}");
        castDelayLabel.SetText($"Cast Delay: {attachedWand.castDelay:0.00}s");
        spreadLabel.SetText($"Spread: {attachedWand.spread:0.0} degrees");
    }

    public void OnWandSocketAttach() {
        IXRSelectInteractable interactable = wandSocketInteractor.firstInteractableSelected;
        GameObject wandObject = (interactable as MonoBehaviour)?.gameObject;
        Wand attachedWand = wandObject.GetComponent<Wand>();

        editTerminal.SetAttachedWand(attachedWand);
        editTerminal.CreateSlots();

        UpdateLabels(attachedWand);
        wandIsAttached = true;
    }

    public void OnWandSocketDetach() {
        editTerminal.UpdateWandSpells();
        editTerminal.ClearSlots();
        editTerminal.SetAttachedWand(null);

        wandIsAttached = false;
    }

    void Update() {
        if (wandIsAttached) {
            for (int i = 0; i < animatedToruses.Count; i++) {
                float rotateSpeed = i % 2 == 0 ? torusRotateSpeed : -torusRotateSpeed;
                Vector3 rotateVector = new Vector3(0, 0, rotateSpeed);
                animatedToruses[i].transform.Rotate(rotateVector);
            }
        }
    }
}