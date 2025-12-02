using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class PrebuiltWandMenu : MonoBehaviour {
    [SerializeField] private WandCreator wandCreator;

    [Header("UI Labels")]
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI castDelayLabel;
    [SerializeField] private TextMeshProUGUI spreadLabel;
    [SerializeField] private TextMeshProUGUI descriptionLabel;

    [Header("Spell Icons")]
    [SerializeField] private Transform iconsBasePosition;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float iconSpacing = 45.0f;
    [SerializeField] private int iconsPerRow = 8;

    [Header("Prebuilt List")]
    [SerializeField] private List<PrebuiltWand> prebuiltWands;


    private int wandIndex = 0;
    private PrebuiltWand currentPrebuilt;

    private List<GameObject> currentIcons = new List<GameObject>();

    public void NextWand() {
        wandIndex = (wandIndex+1) % prebuiltWands.Count;
        UpdateWand();
    }

    public void PreviousWand() {
        wandIndex -= 1;
        if (wandIndex < 0) {
            wandIndex = prebuiltWands.Count-1;
        }
        UpdateWand();
    }

    void UpdateLabels() {
        nameLabel.SetText(currentPrebuilt.wandName);
        castDelayLabel.SetText($"Cast Delay: {currentPrebuilt.castDelay:0.00}s");
        spreadLabel.SetText($"Spread: {currentPrebuilt.spread:0.0} degrees");
        descriptionLabel.SetText(currentPrebuilt.wandDescription);
    }

    void UpdateSpellImages() {
        // Destroy previous icons
        foreach (GameObject obj in currentIcons) {
            Destroy(obj);
        }
        currentIcons.Clear();

        for (int i = 0; i < currentPrebuilt.spells.Count; i++) {
            SpellEntry spellEntry = currentPrebuilt.spells[i];
            if (spellEntry == null) {
                continue;
            }

            Material iconMaterial = spellEntry.iconMaterial;
            GameObject spellIcon = Instantiate(iconPrefab, iconsBasePosition);
            spellIcon.GetComponent<Image>().material = iconMaterial;
            currentIcons.Add(spellIcon);

            float currentX = i % iconsPerRow * iconSpacing;
            float currentY = i / iconsPerRow * -iconSpacing;
            spellIcon.transform.localPosition = new Vector3(currentX, currentY, 0);
        }
    }

    void UpdateWand() {
        currentPrebuilt = prebuiltWands[wandIndex];

        UpdateLabels();
        UpdateSpellImages();

        GameObject prevWand = wandCreator.GetCurrentWand();
        if (prevWand != null) {
            Destroy(prevWand);
        }

        GameObject wandObject = wandCreator.CreateWand(currentPrebuilt.capacity, currentPrebuilt.castDelay, currentPrebuilt.spread);
        var wandScript = wandObject.GetComponent<Wand>();
        wandScript.SetSpells(currentPrebuilt.spells);
        wandScript.UpdateSpellGroups();
    }

    void Awake() {
        if (prebuiltWands.Count == 0) {
            Debug.LogError("PrebuiltWandMenu: No prebuilt wands assigned to list. Please add some.");
        }
    }

    void Start() {
        UpdateWand();
    }
}
