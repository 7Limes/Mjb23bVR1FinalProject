using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class EditTerminal : MonoBehaviour {

    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private Transform slotsBasePosition;

    GameObject attachedWandObject = null;
    Wand attachedWand = null;

    public void OnSocketAttach() {
        if (socketInteractor.hasSelection) {
            IXRSelectInteractable interactable = socketInteractor.firstInteractableSelected;
            attachedWandObject = (interactable as MonoBehaviour)?.gameObject;
            attachedWand = attachedWandObject.GetComponent<Wand>();
            attachedWand.SetIdleAnimation(true);
        }
    }
    
    public void OnSocketDetach() {
        attachedWand.SetIdleAnimation(false);

        attachedWandObject = null;
        attachedWand = null;
    }
}
