using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Necromancer : MonoBehaviour
{
    [Header("~~~~~ Components ~~~~~")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent necro;
    [SerializeField] Animator animator;
    [SerializeField] Transform headPosition;
    [SerializeField] Transform castPosition;
    [SerializeField] LayerMask playerLayer;

    [Header("~~~~~ Necromancer Stats ~~~~~")]
    [SerializeField] int HP;
    [SerializeField] int viewAngle;
    [SerializeField] int viewDistance;
    [SerializeField] int wanderDistance;
    [SerializeField] int wanderDuration;
    [SerializeField] int targetFaceSpeed;
    [SerializeField] float animationSpeed;
    [SerializeField] float attackAnimationDelay;

    [Header("~~~~~ Weapon Stats ~~~~~")]
    [SerializeField] GameObject weapon;
    [SerializeField] Transform weaponHand;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;
    [SerializeField] int weaponDamage;
    [SerializeField] int attackAngle;

    [Header("~~~~~ Magic Stats ~~~~~")]
    [SerializeField] GameObject magicOrbPrefab;
    [SerializeField] Transform skullPositions;
    [SerializeField] float castSpeed;
    [SerializeField] float castRange;
    [SerializeField] int castDamage;
    [SerializeField] int castAngle;

    Vector3 playerDirection;
    Vector3 startingPosition;
    bool playerInRange;
    bool isAttacking;
    bool wanderDestination;
    float angleToPlayer;
    float originalStoppingDistance;
    Transform playerTransform;
    GameObject currentWeapon;
    public PlayerController playerController;

    
    void Start()
    {
        startingPosition = transform.position;
        originalStoppingDistance = necro.stoppingDistance;

        playerTransform = GameManager.instance.transform;
    }

    
    void Update()
    {
        if (necro.isActiveAndEnabled)
        {
            float agentVelocity = necro.velocity.normalized.magnitude;

            animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVelocity, Time.deltaTime * animationSpeed));

            if (playerInRange && canSeePlayer())
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                if (distanceToPlayer <= attackRange && !isAttacking)
                {
                    animator.SetTrigger("Attack");
                    ChooseAttack();
                }
            }
            else
            {
                StartCoroutine(wander());
            }
        }
    }

    IEnumerator attackMelee()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackAnimationDelay);

        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
        if (playerDistance <= attackRange)
        {
            PlayerController player = playerTransform.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(weaponDamage);
            }
        }
        isAttacking = false;
    }

    IEnumerator attackMelee2()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackAnimationDelay);

        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
        if (playerDistance <= attackRange)
        {
            PlayerController player = playerTransform.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(weaponDamage);
            }
        }
        isAttacking = false;
    }

    IEnumerator attackCasting()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackAnimationDelay);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(magicOrbPrefab, castPosition.position, Quaternion.identity);

        }

        isAttacking = false;

    }

    void ChooseAttack()
    {
        if (isAttacking)
            return;

        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

        if (playerDistance <= attackRange)
        {
            int randomMeleeAttack = Random.Range(0, 2);

            if (randomMeleeAttack == 0)
            {
                StartCoroutine(attackMelee());
            }
            else
            {
                StartCoroutine(attackMelee2());
            }
        }
        else
        {
            StartCoroutine(attackCasting());
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            necro.enabled = false;
            animator.SetBool("Death", true);
            StopAllCoroutines();
            Destroy(gameObject);
            Currency.instance.GainDrachma(100);
            MazeState.instance.mini1 = true;
        }
        else
        {
            animator.SetTrigger("Damage");
            StartCoroutine(flashDamage());
            necro.SetDestination(GameManager.instance.player.transform.position);
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * targetFaceSpeed);
    }

    bool canSeePlayer()
    {
        playerDirection = GameManager.instance.player.transform.position - headPosition.position;
        angleToPlayer = Vector3.Angle(playerDirection, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(headPosition.position, playerDirection, out hit, viewDistance, playerLayer))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                necro.stoppingDistance = originalStoppingDistance;
                necro.SetDestination(GameManager.instance.player.transform.position);

                if (necro.remainingDistance <= necro.stoppingDistance)
                {
                    faceTarget();

                    if (!isAttacking && angleToPlayer <= attackAngle)
                    {
                        ChooseAttack();
                    }
                }
                return true;
            }
        }
        necro.stoppingDistance = 0;
        return false;
    }

    IEnumerator wander()
    {
        if (necro.remainingDistance < 0.05f && !wanderDestination)
        {

            wanderDestination = true;
            necro.stoppingDistance = 0;
            yield return new WaitForSeconds(wanderDuration);

            Vector3 randomPosition = Random.insideUnitSphere * wanderDistance;
            randomPosition += startingPosition;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, wanderDistance, 1);
            necro.SetDestination(hit.position);

            wanderDestination = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        Destroy(currentWeapon);
    }
}

