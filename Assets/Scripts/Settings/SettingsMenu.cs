using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class SettingsMenu : MonoBehaviour {
    readonly float[] VIGNETTE_SIZES = {0.85f, 0.6f, 1.0f};

    const int SNAP_TURN = 0;
    const int SMOOTH_TURN = 1;

    [SerializeField] private TunnelingVignetteController vignette;
    [SerializeField] private SnapTurnProvider snapTurn;
    [SerializeField] private ContinuousTurnProvider smoothTurn;

    private GlobalSettings settings;

    void Start() {
        settings = Resources.Load<GlobalSettings>("GlobalSettings");
    }

    public void UpdateVignette(TMP_Dropdown dropdown) {
        vignette.defaultParameters.apertureSize = VIGNETTE_SIZES[dropdown.value];
    }

    public void UpdateTurning(TMP_Dropdown dropdown) {
        switch (dropdown.value) {
            case SNAP_TURN:
                snapTurn.enabled = true;
                smoothTurn.enabled = false;
                break;
            
            case SMOOTH_TURN:
                snapTurn.enabled = false;
                smoothTurn.enabled = true;
                break;
        }
    }

    public void UpdateFlick(Toggle toggle) {
        settings.flickToCastEnabled = toggle.isOn;
    }
}
