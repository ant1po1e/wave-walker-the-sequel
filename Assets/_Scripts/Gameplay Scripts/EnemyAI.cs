using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    public float chaseDuration = 3f;  
    public float speed = 3f;  
    private Transform player;
    private bool isChasing = false;
    private float chaseEndTime;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;  
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);

            if (Time.time > chaseEndTime)
            {
                isChasing = false;
                agent.ResetPath();  
            }
        }
    }

    public void TriggerChase()
    {
        isChasing = true;
        chaseEndTime = Time.fixedTime + chaseDuration;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && isChasing == true)
        {
            isChasing = false;
            agent.ResetPath();
            GameManager.instance.PlayerDead();
        }
    }
}