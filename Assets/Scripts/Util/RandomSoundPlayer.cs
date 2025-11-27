using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour {
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private bool playOnStart = true;

    private AudioSource audioSource;


    bool ErrorChecks() {
        if (audioSource == null) {
            Debug.LogError("RandomSoundPlayer: Could not find AudioSource. Please add one.");
            return false;
        }
        if (soundEffects.Length == 0) {
            Debug.LogError("RandomSoundPlayer: Sound effects array is empty.");
            return false;
        }
        return true;
    }

    public void PlayRandomSound() {
        if (ErrorChecks()) {    
            int randomIndex = Random.Range(0, soundEffects.Length);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
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