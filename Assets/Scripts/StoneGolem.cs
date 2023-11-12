using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StoneGolem : MonoBehaviour, IDamage
{
    [Header("~~~~~ Components ~~~~~")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent stoneGolem;
    [SerializeField] Animator animator;
    [SerializeField] Transform headPosition;
    [SerializeField] LayerMask playerLayer;

    [Header("~~~~~ Stone Golem Stats ~~~~~")]
    [SerializeField] int HP;
    [SerializeField] int viewAngle;
    [SerializeField] int viewDistance;
    [SerializeField] int wanderDistance;
    [SerializeField] int wanderDuration;
    [SerializeField] int targetFaceSpeed;
    [SerializeField] float animationSpeed;
    [SerializeField] float animationDelay;

    [Header("~~~~~ Attack Stats ~~~~~")]
    [SerializeField] GameObject stone;
    [SerializeField] Transform throwingHand;
    [SerializeField] GameObject punchingHand;
    [SerializeField] Transform punchingHandPosition;
    [SerializeField] int stoneDamage;
    [SerializeField] int punchDamage;
    [SerializeField] float attackRate;
    [SerializeField] float attackRange;
    [SerializeField] int attackAngle;

    Vector3 playerDirection;
    Vector3 startingPosition;
    bool playerInRange;
    bool isAttacking;
    bool wanderDestination;
    float originalStoppingDistance;
    float angleToPlayer;
    float originalSpeed;
    Transform playerTransform;
    GameObject currentStone;
    public PlayerController playerController;


    
    void Start()
    {
        startingPosition = transform.position;
        originalStoppingDistance = stoneGolem.stoppingDistance;

        playerTransform = GameManager.instance.player.transform;
    }

    
    void Update()
    {
        if (stoneGolem.isActiveAndEnabled)
        {
            float agentVelocity = stoneGolem.velocity.normalized.magnitude;
            animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVelocity, Time.deltaTime * animationSpeed));

            if (playerInRange && CanSeePlayer())
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                if (distanceToPlayer <= attackRange && !isAttacking)
                {
                    animator.SetTrigger("Attack");
                    ChooseAttack();
                }
            }
            else StartCoroutine(Wander());
        }
    }

    IEnumerator Wander()
    {
        if (stoneGolem.remainingDistance < 0.05f && !wanderDestination) 
        {
            wanderDestination = true;
            stoneGolem.stoppingDistance = 0;
            yield return new WaitForSeconds(wanderDuration);

            Vector3 randomPosition = Random.insideUnitSphere * wanderDistance;
            randomPosition += startingPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, wanderDistance, 1);
            stoneGolem.SetDestination(hit.position);

            wanderDestination = false;
        }
    }

    bool CanSeePlayer()
    {
        playerDirection = GameManager.instance.player.transform.position - headPosition.position;
        angleToPlayer = Vector3.Angle(playerDirection, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(headPosition.position, playerDirection, out hit, viewDistance, playerLayer))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle) 
            {
                stoneGolem.stoppingDistance = originalStoppingDistance;
                stoneGolem.SetDestination(GameManager.instance.player.transform.position);

                if (stoneGolem.remainingDistance <= stoneGolem.stoppingDistance)
                {
                    FaceTarget();

                    if (!isAttacking && angleToPlayer <= attackAngle) 
                    {
                        ChooseAttack(); //will write this function once throwStone() is done

                    }
                }
                return true;
            }
        }
        stoneGolem.stoppingDistance = 0;
        return false;
    }

    IEnumerator attackMelee()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(animationDelay);

        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
        if (playerDistance <= attackRange)
        {
            PlayerController player = playerTransform.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(punchDamage);
            }
        }
        isAttacking = false;
    }

    public GameObject createStone()
    {
        GameObject newStone = Instantiate(stone, throwingHand.position, transform.rotation);
        return newStone;
    }

    IEnumerator throwStone()
    {
        isAttacking = true;
        animator.SetTrigger("Throw");
        yield return new WaitForSeconds(animationDelay);
        createStone();
        isAttacking = false;
    }

    public void ChooseAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(attackMelee());
        }
        else
        {
            StartCoroutine(throwStone());
        }
    }
    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * targetFaceSpeed);
    }

    public void physics(Vector3 direction) 
    {
        stoneGolem.velocity = direction / 3;
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
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator damageFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    IEnumerator stopMoving()
    {
        stoneGolem.speed = 0;
        yield return new WaitForSeconds(0.1f);
        stoneGolem.speed = originalSpeed;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        stoneGolem.SetDestination(GameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            
            stoneGolem.enabled = false;
            stopMoving();
            animator.SetBool("Death", true);
            Destroy(gameObject);
            Currency.instance.GainDrachma(100);
        }
        else
        {

            animator.SetTrigger("Damage");
            StartCoroutine(damageFlash());
            stoneGolem.SetDestination(GameManager.instance.player.transform.position);

        }
    }
}
