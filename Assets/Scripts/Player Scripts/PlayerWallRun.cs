using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalDirection;
    private float verticalDirection;

    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    public Transform orientation;
    private PlayerController pc;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        CheckForWalls();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (pc.wallruning)
        {
            WallRunningMovement();
        }
    }

    private void CheckForWalls()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        //get inputs
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        verticalDirection = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // wallrun Start
        if ((wallLeft || wallRight) && verticalDirection > 0 && AboveGround())
        {
            if (!pc.wallruning)
            {
                BeginWallRun();
            }
        }

        // not wallrunning
        else
        {
            if (pc.wallruning)
            {
                EndWallRun();
            }
        }
    }

    private void BeginWallRun()
    {
        pc.wallruning = true;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //Forward momentum
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //upwards downward force
        if (upwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }

        if (downwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }

        //push to wall force
        if (!(wallLeft && horizontalDirection > 0) && !(wallRight && horizontalDirection < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
        
    }

    private void EndWallRun()
    {

    }
}
