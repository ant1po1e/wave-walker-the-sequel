using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPulse : MonoBehaviour
{
    public GameObject sonarPrefab;

    [SerializeField]
    private float fireRate = 2f;
    [SerializeField]
    private float canFire = -1f;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time > canFire || Input.GetMouseButtonDown(0) && Time.time > canFire) 
        {
            Instantiate(sonarPrefab, transform.position, Quaternion.identity);
            canFire = Time.time + fireRate;

            AudioManager audioManager = AudioManager.instance;
            audioManager.PlaySFX("sonarPulse");
        }
    }
}
