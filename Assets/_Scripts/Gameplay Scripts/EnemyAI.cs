using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float chaseDuration = 3f;
    public float speed = 3f;

    private Transform player;
    private bool isChasing = false;
    private float chaseEndTime;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            if (Time.time > chaseEndTime)
            {
                isChasing = false;
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void TriggerChase()
    {
        isChasing = true;
        chaseEndTime = Time.time + chaseDuration;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && isChasing)
        {
            isChasing = false;
            rb.velocity = Vector2.zero;
            GameManager.instance.PlayerDead();
        }
    }
}