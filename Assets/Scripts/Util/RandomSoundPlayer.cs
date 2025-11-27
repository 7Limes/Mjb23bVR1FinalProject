using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour {
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool disableRepeats = true;

    private AudioSource audioSource;
    private int lastIndex = -1;


    bool ErrorChecks() {
        if (audioSource == null) {
            Debug.LogError("RandomSoundPlayer: Could not find AudioSource. Please add one.");
            return false;
        }
        if (soundEffects.Length == 0) {
            Debug.LogError("RandomSoundPlayer: Sound effects array is empty.");
            return false;
        }
        if (disableRepeats && soundEffects.Length < 2) {
            disableRepeats = false;
        }
        return true;
    }

    public void PlayRandomSound() {
        if (ErrorChecks()) {
            int index;
            do {
                index = Random.Range(0, soundEffects.Length);
            } while (index == lastIndex);

            audioSource.PlayOneShot(soundEffects[index]);

            if (disableRepeats) {
                lastIndex = index;
            }
        }
    }

    public void PlayRandomSound(float pitch, float volume) {
        if (ErrorChecks()) {    
            int randomIndex = Random.Range(0, soundEffects.Length);
            float savedPitch = audioSource.pitch;
            
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(soundEffects[randomIndex], volume);
            audioSource.pitch = savedPitch;
        }
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        if (playOnStart) {
            PlayRandomSound();
        }
    }
}