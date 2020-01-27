using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LL.FSM;

public class PlayerController : EntityMovement {

    public enum JumpType {
        Standard,
        Variable
    }

    public enum MoveState {
        Standing,
        Running,
        Dashing
    }


    [Header("Temp Jump Variables")]
    public JumpType jumpType = JumpType.Standard;
    public float jumpForce = 1200f;
    public float arialJumpForce = 500f;

    public int maxJumpCount = 1;

    [Header("Variable Jump Variables")]
    public float descendingFallMod = 1.5f;
    public float ascendingFallMod = 1f;
    public float wallSlideFallMod = 0.5f;
    public float maxFallSpeed = 20f;

    [Header("Dash Variables")]
    public float dashDuration = 0.5f;
    //public float dashSpeed = 30f;
    public float dashCooldown = 3f;




    public System.Action onCollideWithGround;

    public override void Initialize(Entity owner) {
        base.Initialize(owner);
    }

    private void Start() {
        //FSM TESTING
        FSMState normalState = Owner.FSMManager.GetState("PlayerNormal");
        if (normalState != null)
            Owner.EntityFSM.ChangeState(normalState);
        else {
            Debug.LogError("Can't find normal state");
        }
    }



    protected override void Update() {
        base.Update();
    }

    protected override void ConfigureHorizontalDirection() {

        if (StatusManager.CheckForStatus(Owner.gameObject, Constants.StatusType.MovementAffecting) == true) {

            //currentHorizontalDirection = Facing == FacingDirection.Left ? -1 : 1;
            return;

        }
        else if (StatusManager.CheckForStatus(Owner.gameObject, Constants.StatusType.ForceMaxSpeed) == true) {
            currentHorizontalDirection = Facing == FacingDirection.Left ? -1 : 1;
        }
        else {
            currentHorizontalDirection = GameInput.Horizontal;
        }


        if (RayController.IsHittingWall && RayController.IsGrounded == false && MyBody.velocity.y <= 0) {
            currentHorizontalDirection = 0f;
        }
        UpdateFacing();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

        if (jumpType == JumpType.Variable)
            VariableFall();
    }


    protected override void UpdateFacing() {

        //if(RayController.IsWallSliding == true && GameInput.Horizontal < 0 && Owner.SpriteRenderer.flipX == true) {
        //    Owner.SpriteRenderer.flipX = false;
        //    return;
        //}

        //if (RayController.IsWallSliding == true && GameInput.Horizontal > 0 && Owner.SpriteRenderer.flipX == false) {
        //    Owner.SpriteRenderer.flipX = true;
        //    return;
        //}


        if (GameInput.Horizontal < 0 && Owner.SpriteRenderer.flipX == false) {
            Owner.SpriteRenderer.flipX = true;
            SwapWeaponSide();
        }

        if (GameInput.Horizontal > 0 && Owner.SpriteRenderer.flipX == true) {
            Owner.SpriteRenderer.flipX = false;
            SwapWeaponSide();
        }
    }


    private void VariableFall() {

        Owner.AnimHelper.PlayOrStopAnimBool("WallSliding", RayController.IsWallSliding);

        Vector2 desiredFallVelocity = Vector2.zero;

        if (RayController.IsWallSliding == true) {
            desiredFallVelocity = Vector2.up * Physics2D.gravity.y * wallSlideFallMod * Time.deltaTime;
        }
        else if (MyBody.velocity.y < 0) {
            desiredFallVelocity = Vector2.up * Physics2D.gravity.y * descendingFallMod * Time.deltaTime;
        }
        else if (MyBody.velocity.y > 0 && GameInput.JumpHeld == false) {
            desiredFallVelocity = Vector2.up * Physics2D.gravity.y * ascendingFallMod * Time.deltaTime;
        }


        MyBody.velocity += desiredFallVelocity;


        if (RayController.IsWallSliding == true && MyBody.velocity.y <= -maxFallSpeed * wallSlideFallMod) {
            MyBody.velocity = new Vector2(MyBody.velocity.x, -maxFallSpeed * wallSlideFallMod);
        }
        else if (MyBody.velocity.y <= -maxFallSpeed) {
            MyBody.velocity = new Vector2(MyBody.velocity.x, -maxFallSpeed);
        }


    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (LayerTools.IsLayerInMask(groundLayer, other.gameObject.layer) == false)
            return;

        //Debug.Log(RayController.IsHittingWall + " is hitting wall");
        //Debug.Log(RayController.IsGrounded + " is grounded");

        //if (RayController.IsHittingWall)
        //    return;

        //Debug.Log("Collided with ground");
        //Vector2 point = other.contacts[0].point;

        float lowestPoint = other.contacts[0].point.y;
        int count = other.contacts.Length;
        for (int i = 0; i < count; i++) {

            //Debug.Log(other.contacts[i].point.x);

            if (other.contacts[i].point.y < lowestPoint)
                lowestPoint = other.contacts[i].point.y;

        }


        //float xPos = other.contacts[0].point.x;
        //float myXPos = transform.position.x;

        //if(xPos > myXPos) {
        //    float diff = xPos - myXPos;

        //    Debug.Log("Amount to the Right: " + diff);
        //}
        //else {
        //    float diff = myXPos - xPos;
        //    Debug.Log("Amount to the Left: " + diff);
        //}


        //float adjustedMyX = transform.position.x - (BoxCollider.bounds.size.x / 2f);



        //Debug.Log(lowestPoint + " is the contact y pos");
        //Debug.Log((transform.position.y /*- ((BoxCollider.bounds.size.y / 2))*/) + " is my y pos");

        if (lowestPoint < transform.position.y /*- ((BoxCollider.bounds.size.y / 2))*/) {
            //Debug.Log("Collided With Somthing Below");
            onCollideWithGround?.Invoke();
        }

    }








}