using UnityEngine;
using System.Collections;

public class CanvasFadeToggle : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine fadeCoroutine;
    private bool isVisible = false;

    void Start() {
        // Initialize canvas group if not assigned
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasObject == null)
            canvasObject = gameObject;
    }

    public void ToggleCanvas() {
        if (isVisible)
            FadeOut();
        else
            FadeIn();
    }

    public void FadeIn() {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCanvasIn());
    }

    public void FadeOut() {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCanvasOut());
    }

    private IEnumerator FadeCanvasIn() {
        // Enable the canvas object first
        canvasObject.SetActive(true);

        // Initialize alpha to 0 if this is the first fade in
        if (canvasGroup.alpha > 0.9f)
            canvasGroup.alpha = 0f;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isVisible = true;
    }

    private IEnumerator FadeCanvasOut() {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;

        // Disable the canvas object after fade is complete
        canvasObject.SetActive(false);
        isVisible = false;
    }
}