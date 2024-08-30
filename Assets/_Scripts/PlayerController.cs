
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public GameObject sonarPrefab;

    [SerializeField]
    private float fireRate = 2f;
    [SerializeField]
    private float canFire = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire) 
        {
            Instantiate(sonarPrefab, transform.position, Quaternion.identity);
            canFire = Time.time + fireRate;

            AudioManager audioManager = AudioManager.instance;
            audioManager.PlaySFX("sonarPulse");
        }
        
        ProcessInput();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
