using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public float startHealth = 100;
    public float maxHealth = 100;
    public bool isDead = false;
    [Space]
    public float minWalkWaitTime = 0.3f;
    public float maxWalkWaitTime = 1.5f;
    private float walkWaitTimer;
    public float minWalkTime = 0.3f;
    public float maxWalkTime = 1.5f;
    private float walkTimer;
    [Space]
    public float walkSpeed = 6f;
    [Space]
    public float attackDamage = 10f;
    public float attackDistance = 2f;
    public float timeBetweenAttacks = 0.75f;
    private float timerBetweenAttacks = 0.75f;

    private bool initWalk;

    private Vector3 walkDirection;

    private Transform playerPos;
    private PlayerController player;
    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = playerPos.GetComponent<PlayerController>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            Destroy(gameObject);

        if (Physics.Raycast(transform.position, playerPos.position - transform.position, Vector3.Distance(transform.position, playerPos.position)))
        {
            if (Vector3.SqrMagnitude(playerPos.position - transform.position) > ((attackDistance * 0.5f) * (attackDistance * 0.5f)))
                charController.Move((playerPos.position - transform.position).normalized * walkSpeed * Time.deltaTime);

            if (timerBetweenAttacks <= 0 && Vector3.SqrMagnitude(playerPos.position - transform.position) < (attackDistance * attackDistance))
            {
                player.TakeDamage(attackDamage);
                timerBetweenAttacks = timeBetweenAttacks;
            }
            else
                timerBetweenAttacks -= Time.deltaTime;
        }
        else
        {
            if (walkWaitTimer <= 0)
            {
                if (!initWalk)
                {
                    walkTimer = Random.Range(minWalkTime, maxWalkTime);
                    walkDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                    initWalk = true;
                }

                if (walkTimer <= 0)
                {
                    walkWaitTimer = Random.Range(minWalkWaitTime, maxWalkWaitTime);
                }
                else
                {
                    walkTimer -= Time.deltaTime;
                    charController.Move(walkDirection * walkSpeed * Time.deltaTime);
                }
            }
            else
            {
                walkWaitTimer -= Time.deltaTime;
                initWalk = false;
            }
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;

        if (health > maxHealth)
            health = maxHealth;
    }
}
