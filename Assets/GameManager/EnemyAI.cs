using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Attacking }
    private EnemyState currentState = EnemyState.Patrolling;


    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.2f;
    
    
    public int attackDamage = 10;
    
    private Transform player;
    private Vector2 randomDirection;
    private Rigidbody2D rb;
    private float nextAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        /*transiciones entre estados segun la distancia al jugador*/
        switch (currentState)
        {
            case EnemyState.Patrolling:
                if (distanceToPlayer < detectionRange)
                {
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                if (distanceToPlayer < attackRange)
                {
                    currentState = EnemyState.Attacking;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = EnemyState.Patrolling;
                }
                break;

            case EnemyState.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    currentState = EnemyState.Chasing;
                }
                break;
        }
    }

    void FixedUpdate()
    { // ejecutar la logica del estado actual
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }
    /*metodo para el movimiento aleatorio en el estado de patrullaje*/
    void Patrol()
    {
        rb.linearVelocity = new Vector2(randomDirection.x * patrolSpeed, 0);
    }
    /*metodo del movimiento hacia el jugador en el estado de persecucion*/
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;
    }
    /*metodo que permite el ataque al jugador si esta en el rango y el cooldown lo permite*/
    void Attack()
    {
        if (Time.time > nextAttackTime)
        {
            Debug.Log("Enemy attacks the player!");
            ScriptGameManager.instance.RestarPuntosV(attackDamage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    /*rutina que cambia la direccion del enemigo de manera aleatoria*/
    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }
}
