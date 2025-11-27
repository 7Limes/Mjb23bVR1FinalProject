using UnityEngine;
using UnityEngine.Events;

public class ImpactSoundPlayer : MonoBehaviour {
    [SerializeField] private float impactForceThreshold = 5.0f;

    [SerializeField] private float pitchFactor = 0.5f;
    [SerializeField] private float maxPitch = 3.0f;

    [SerializeField] private float volumeFactor = 0.5f;
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] private RandomSoundPlayer soundPlayer;

    void OnCollisionEnter(Collision collision) {
        float magnitude = collision.impulse.magnitude;
        if (magnitude > impactForceThreshold) {
            float pitch = Mathf.Clamp(magnitude * pitchFactor, 0.0f, maxPitch);
            float volume = Mathf.Clamp(magnitude * volumeFactor, 0.0f, maxVolume);
            soundPlayer.PlayRandomSound(pitch, volume);
        }
    }
}