using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashSpeed = 20.0f;

    private Vector3 dashDirection;
    private bool isDashing;

    private void StartDash(Vector3 playerForward)
    {
        if (isDashing)
            return;

        isDashing = true;
        dashDirection = playerForward.normalized;
        StartCoroutine(DashMovement());
    }

    private IEnumerator DashMovement()
    {
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            controller.Move(dashSpeed * Time.deltaTime * dashDirection);
            dashTime += Time.deltaTime;
            yield return null;

        }

        isDashing = false;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}
