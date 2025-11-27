using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour {
    public AudioClip[] soundEffects;

    void Start() {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (soundEffects.Length > 0) {
            int randomIndex = Random.Range(0, soundEffects.Length);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
        }
    }
}