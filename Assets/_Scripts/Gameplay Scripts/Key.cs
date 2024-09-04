using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        { 
            AudioManager audioManager = AudioManager.instance;
            audioManager.PlaySFX("key");
            Door.instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}
