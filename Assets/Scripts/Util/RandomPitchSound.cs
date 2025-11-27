using UnityEngine;

public class RandomPitchSound : MonoBehaviour {
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.2f;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) {
            Debug.LogError("RandomPitchSound: No AudioSource component found on " + gameObject.name);
            return;
        }

        if (audioSource != null) {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }
}