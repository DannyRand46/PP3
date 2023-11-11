//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;


public class PlayerController : MonoBehaviour, IDamage
{
    // orgization of materials in inspector 
    [Header("----Components----")]
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource aud;
    [SerializeField] Transform groundRaySource;

    [Header("----Player Stats----")]
    [Range(1, 50)][SerializeField] float Hp;
    [Range(1, 20)][SerializeField] private float playerSpeed;
    [Range(1, 3)][SerializeField] private float sprintMod;
    [Range(1, 3)][SerializeField] int jumpMax;
    [Range(8, 30)][SerializeField] private float jumpHeight;
    //[Range(-10, 40)][SerializeField] private float gravityMod;
    [Range(-10, -40)][SerializeField] private float gravityValue;

    [Header("----Audio----")]
    [SerializeField] AudioClip[] AudDamage;
    [Range(0, 1)][SerializeField] float audDamagevol;
    [SerializeField] AudioClip[] AudJump;
    [Range(0, 1)][SerializeField] float audJumpvol;
    [SerializeField] AudioClip[] AudFootSteps;
    [Range(0, 1)][SerializeField] float audFootStepsvol;

    [Header("~~~~~ Power Ups ~~~~~")]
    [SerializeField] public bool damageReduction = false;
    [SerializeField] public float damageReductionEffect = 0.2f;
    [SerializeField] public bool shielded = false;
    [SerializeField] public bool speedBoosted = false;
    [SerializeField] public float speedBoostEffect = 0.25f;
    [SerializeField] public bool healthBoosted = false;
    [SerializeField] public float healthBoostEffect = 0.2f;
    [SerializeField] bool isInvisible = false;
    [SerializeField] float timeInvisible = 30f;
    [SerializeField] float invisibleCooldown = 30f;
    [SerializeField] float invisibleCooldownTimer = 0f;

    // private variabels 
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector3 move;
    private int jumpedtimes;
    bool isSprinting;
    float HPOrig;
    int Layer_Mask;
    bool Crouching;
    bool footstepsPlaying;
    //slide
    //private PlayerSlide playerSlide;
    //dash
    private PlayerDash playerDash;

    private Coroutine invisibilityCoroutine;
    private Coroutine shieldCoroutine;

    //wallrun
    public bool wallruning;

    private void Start()
    {
        
        Layer_Mask = LayerMask.GetMask("Wall") + LayerMask.GetMask("Ground");
        HPOrig = Hp;
        spawnPlayer();
        playerDash = GetComponent<PlayerDash>();
    }


    void Update()
    {

        //if its not paused do this 
        if (!GameManager.instance.isPaused)
        {
            //CheatsyDoodle();
            
            movement();
            Sprint();
            //dash
            if(Input.GetButtonDown("Dash"))
            {
                if(playerDash != null && !playerDash.IsDashing())
                {
                    Vector3 dashDirection = move.normalized;
                    
                    if(dashDirection == Vector3.zero)
                    {
                        dashDirection = transform.forward;
                    }

                    playerDash.StartDash(dashDirection);
                }
            }

            if (invisibleCooldownTimer > 0f)
            {
                invisibleCooldown -= Time.deltaTime;
            }
        }
    }

    //  controls the players movement 
    void movement()
    {
        //Addational movement called here 
        Sprint();

        //WallRun();

        Crouched();
        //checks to make sure player is grounded
        RaycastHit GroundCheck;
        Debug.DrawRay(groundRaySource.position, transform.TransformDirection(Vector3.down * 0.1f));
        if (groundedPlayer && move.normalized.magnitude > 0.3f && !footstepsPlaying)
        {
            StartCoroutine(PlayFootSteps());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out GroundCheck, 1.1f, Layer_Mask))
        {
            groundedPlayer = true;
            //sets the players up and down velocity to 0 
            playerVelocity.y = 0f;
            //rests jump to 0 once player lands
            jumpedtimes = 0;
        }
        else
        {
            groundedPlayer = false;
        }

        //vector 2 that recives are players input and moves it to that  postion 
        move = (Input.GetAxis("Horizontal") * transform.right) +
             (Input.GetAxis("Vertical") * transform.forward);

        //syncs are times across computers for performaces 
        controller.Move(move * Time.deltaTime * playerSpeed);
        

        //will take a button input thats press down 
        if (Input.GetButtonDown("Jump") && jumpedtimes <= jumpMax)
        {
            if (Crouching == true)
            {
                Crouching = false;
                playerSpeed *= sprintMod;
                controller.height *= 2;
            }
            //will assighn are y to some height 
            playerVelocity.y = jumpHeight;
            //play sound
            aud.PlayOneShot(AudJump[Random.Range(0, AudJump.Length)], audJumpvol);
            //and increment jump
            jumpedtimes++;
            //Reset stamina regen?
            GameManager.instance.player.GetComponent<PlayerStamina>().ResetRegen();
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        UpdatePlayerUi();
    }

    IEnumerator PlayFootSteps()
    {
        footstepsPlaying = true;
        aud.PlayOneShot(AudFootSteps[Random.Range(0, AudFootSteps.Length)], audFootStepsvol);

        if (!isSprinting)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
            yield return new WaitForSeconds(0.3f);

        footstepsPlaying = false;
    }

    // addtinal method  for are walk 
    // will incremnet the player speed as
    //as long as they hold the button;
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint") && !isSprinting)
        {
            //if true  increment the player speed by some number 
            playerSpeed *= sprintMod;
            isSprinting = true;
            GameManager.instance.player.GetComponent<PlayerStamina>().ResetRegen();
        }
        else if (Input.GetButtonUp("Sprint") && isSprinting)
        {
            //if false Decrement the player speed 
            playerSpeed /= sprintMod;
            isSprinting = false;
            GameManager.instance.player.GetComponent<PlayerStamina>().ResetRegen();
        }
        //slide check
        if(isSprinting && Input.GetButtonDown("Slide") && groundedPlayer)
        {
            //start slide
            PlayerSlide playerSlideComponent = GetComponent<PlayerSlide>();
            //Vector3 slideDirection = transform.forward;

            if(playerSlideComponent != null && !playerSlideComponent.IsSliding())
            {
                playerSlideComponent.StartSlide(playerSpeed, transform.forward);
            }
        }
    }

    public void DisableSprint()
    {
        isSprinting = false;
        playerSpeed /= sprintMod;
    }
   
    void Crouched()
    {
        //check if grouded check button if false
        if (groundedPlayer && Input.GetButtonDown("Crouch") && Crouching == false)
        {
            Crouching = true;
            //change local y scale
            controller.height /= 2;
            //decrement speed
            playerSpeed /= sprintMod;

        }//check if grouded check button if true
        else if (groundedPlayer && Input.GetButtonDown("Crouch") && Crouching == true)
        {
            Crouching = false;
            //set height back to normal 
            controller.height *= 2;
            //give player back speed 
            playerSpeed *= sprintMod;
        }
    }

    void UpdatePlayerUi()
    {
        if (GameManager.instance.playerStaminaBar.fillAmount == 1.0f)
        {
            GameManager.instance.playerStaminaBar.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            GameManager.instance.playerStaminaBar.transform.parent.gameObject.SetActive(true);
        }

        GameManager.instance.playerHPBar.fillAmount = (float)Hp / (float)HPOrig;
    }

    public void TakeDamage(int amount)
    {
        if (shielded || playerDash != null && playerDash.IsDashing())
        { return; }

        if (damageReduction)
        {
            amount = Mathf.CeilToInt(amount * (1f - damageReductionEffect));
        }

        StartCoroutine(GameManager.instance.flash());
        Hp -= amount;
        UpdatePlayerUi();
        if (Hp <= 0)
        {
            GameManager.instance.youLose();
        }
    }

    public void AddHealth(float amount)
    {
        Hp += amount;
        if (Hp >= HPOrig)
        {
            Hp = HPOrig;
        }
    }

    public void spawnPlayer()
    {
        Hp = HPOrig;
        UpdatePlayerUi();
        controller.enabled = false;
        transform.position = GameManager.instance.playerSpawn.transform.position;
        controller.enabled = true;
    }

    public bool GetGrounded()
    {
        return groundedPlayer;
    }

    public bool IsSprinting()
    {
        return isSprinting;
    }

    //Cheats for devs to play around with
    void CheatsyDoodle()
    {
        if (Input.GetButton("Submit"))
        {
            controller.enabled = false;
            transform.position = GameManager.instance.devCheat.transform.position;
            controller.enabled = true;
        }
        if(Input.GetButton("L"))
        {
            controller.enabled = false;
            transform.position = GameManager.instance.lightningCheat.transform.position;
            controller.enabled = true;
        }
        if(Input.GetButton("F"))
        {
            controller.enabled = false;
            transform.position = GameManager.instance.fireCheat.transform.position;
            controller.enabled = true;
        }
        if(Input.GetButton("T"))
        {
            controller.enabled = false;
            transform.position = GameManager.instance.treasureCheat.transform.position;
            controller.enabled = true;
        }
    }

    public void DamageReduction()
    {
        damageReduction = true;
    }

    public void DamageReductionOff()
    { 
        damageReduction = false; 
    }

    public void ShieldOn()
    {
        if (!shielded)
        {
            shielded = true;

            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(ShieldRoutine());
        }

    }

    public void ShieldOff()
    {
        if (shielded)
        {
            shielded = false;

            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
                shieldCoroutine = null;
            }
        }
    }
    public IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(5);
        ShieldOff();
        yield return new WaitForSeconds(25);
        ShieldOn();
    }

    public void SpeedBoostOn()
    {
        speedBoosted = true;
        playerSpeed *= (1 + speedBoostEffect);
    }

    public void SpeedBoostOff()
    {
        if (speedBoosted)
        {
            playerSpeed /= (1 + speedBoostEffect);
            speedBoosted = false;
        }
    }

    public void HealthBoostOn()
    {
        if (!healthBoosted)
        {
            healthBoosted = true;
            HPOrig *= (1 + healthBoostEffect);
            Hp *= (1 + healthBoostEffect);

            UpdatePlayerUi();
        }
    }

    public void HealthBoostOff()
    {
        if (healthBoosted)
        {
            healthBoosted = false;
            HPOrig /= (1 + healthBoostEffect);
            Hp /= (1 + healthBoostEffect);

            if (Hp > HPOrig)
            {
                Hp = HPOrig;
            }

            UpdatePlayerUi();
        }
    }

    public void InvisibilityActive()
    {
        if (!isInvisible && invisibleCooldownTimer <= 0f)
        {
            if (invisibilityCoroutine != null)
            {
                StopCoroutine(invisibilityCoroutine);
            }

            invisibilityCoroutine = StartCoroutine(InvisibilityEffect());
        }
    }

    public IEnumerator InvisibilityEffect()
    {
        isInvisible = true;
        invisibleCooldownTimer = invisibleCooldown;
        yield return new WaitForSeconds(timeInvisible);

        isInvisible = false;

        while (invisibleCooldownTimer > 0)
        {
            invisibleCooldownTimer -= Time.deltaTime;
            yield return null;

            invisibilityCoroutine = null;
        }
    }

    public void InvisibilityOff()
    {
        if (isInvisible)
        {
            if (invisibilityCoroutine != null)
            {
                StopCoroutine(invisibilityCoroutine);
                invisibilityCoroutine = null;
            }

            isInvisible = false;
            invisibleCooldownTimer = invisibleCooldown;
        }
    }

    public bool IsInvisible() { return isInvisible; }


}
