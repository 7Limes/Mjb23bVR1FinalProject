using UnityEngine;
using System.Collections.Generic;

public class DestroyBox : MonoBehaviour {
    [SerializeField] private List<string> whitelistedTags = new List<string>();

    void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;

        // Check if obj has a whitelisted tag
        foreach (string tag in whitelistedTags) {
            if (obj.CompareTag(tag)) {
                return;
            }
        }
        
        Destroy(obj);
    }
}