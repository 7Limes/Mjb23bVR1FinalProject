using UnityEngine;
using System.Collections.Generic;

public class TeleportBox : MonoBehaviour {
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private List<string> whitelistedTags = new List<string>();

    void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;

        foreach (string tag in whitelistedTags) {
            if (obj.CompareTag(tag)) {
                obj.transform.position = teleportPoint.position;
                return;
            }
        }
    }
}