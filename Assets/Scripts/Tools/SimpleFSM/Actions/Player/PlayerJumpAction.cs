using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJumpAction : BaseStateAction {

    private PlayerController playerController;
    private int currentJumpCount;
    private Action jumpResetCallBack;

    private bool jumped;

    public PlayerJumpAction(Entity owner, bool runUpdate) : base(owner, runUpdate) {
        currentJumpCount = 0;
        playerController = owner.Movement as PlayerController;
    }

    public override void RegisterEvents() {
        //playerController.onCollideWithGround += ResetJump;
        playerController.RayController.onGroundedAction += ResetJump;
    }

    public override void UnregisterEvents() {
        //playerController.onCollideWithGround -= ResetJump;
        playerController.RayController.onGroundedAction -= ResetJump;
    }

    public override void Execute() {
        if (GameInput.Jump) {
            Jump();
        }

        //ResetJump();

        //Debug.Log(currentJumpCount + " is current jumps");
    }

    public override void ManagedUpdate() {
        base.ManagedUpdate();

        float vSpeed = owner.Movement.MyBody.velocity.y;

        owner.AnimHelper.SetFloat("vSpeed", vSpeed);
    }


    private void Jump() {
        //Debug.Log("Trying to Jump");

        float desiredJumpForce = 0;
        Vector2 desiredJumpVector = new Vector2(0f, desiredJumpForce);

        if (playerController.RayController.IsWallSliding == true) {
            desiredJumpForce = playerController.jumpForce;

            float xSign = playerController.Facing == EntityMovement.FacingDirection.Left ? 1 : -1;

            desiredJumpVector = new Vector2((playerController.jumpForce /2f) * xSign, desiredJumpForce);
            //Debug.Log("Wall Jumping");
            playerController.RayController.onWallJump?.Invoke();
        }
        else if (playerController.RayController.IsGrounded == true) {
            desiredJumpForce = playerController.jumpForce;
            desiredJumpVector = new Vector2(0f, desiredJumpForce);
            //Debug.Log("Regular Jumping");
        }
        else if (playerController.RayController.IsGrounded == false) {
            desiredJumpForce = playerController.arialJumpForce;
            desiredJumpVector = new Vector2(0f, desiredJumpForce);
            //Debug.Log("Air Jumping");
        }


        if (currentJumpCount >= playerController.maxJumpCount) {
            //Debug.Log("TOo many jumps: " + currentJumpCount);
            return;
        }


        owner.AnimHelper.PlayOrStopAnimBool("Jumping", true);

        playerController.MyBody.velocity = new Vector2(playerController.MyBody.velocity.x, 0f);

        playerController.MyBody.AddForce(desiredJumpVector /*Vector2.up * desiredJumpForce*/);

        currentJumpCount++;
        //jumped = true;
    }

    private void ResetJump() {
        //if (jumped == false)
        //    return;

        //Debug.Log("Landing");
        //owner.AnimHelper.PlayAnimTrigger("Land");

        if (/*playerController.MyBody.velocity.y <= 0f &&*/ currentJumpCount > 0 /*&& playerController.RayController.IsGrounded == true*/) {
            //Debug.Log("Reseting jump");

            currentJumpCount = 0;
            //owner.AnimHelper.PlayAnimTrigger("Land");
            owner.AnimHelper.PlayOrStopAnimBool("Jumping", false);
            //jumped = false;
        }
    }
}
