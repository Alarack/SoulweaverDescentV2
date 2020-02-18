using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EntityMovement {

    public float zombieWanderInterval;
    [Range(0f, 1f)]
    public float zombieFlipChance;

    public float turnPauseTime;

    private bool recentFlip;

    private Timer flipCooldown;
    private Timer turnPauseTimer;
    private float turnPauseTimeCurrent;
    public bool pauseWalk;


    public override void Initialize(Entity owner) {
        base.Initialize(owner);
        flipCooldown = new Timer(0.25f, ResetFlipTimer, true);
        //turnPauseTimer = new Timer(turnPauseTime, ResetPauseTimer, false);
        //turnPauseTimeCurrent = turnPauseTime;
    }

    protected override void Update() {
        base.Update();

        //if (recentFlip == true && flipCooldown != null) {
        //    flipCooldown.UpdateClock();
        //}

        //if(pauseTurning == true && turnPauseTimer != null){
        //    turnPauseTimer.UpdateClock();
        //}
    }

    //public override void MoveHorizontal()
    //{
    //    base.MoveHorizontal();
    //}

    public override void MoveHorizontal() {

        if (pauseWalk == true) {
            Owner.AnimHelper.StopWalk();
            return;
        }


        base.MoveHorizontal();
    }

    protected override void ConfigureHorizontalDirection() {

        if (StatusManager.CheckForStatus(Owner.gameObject, Constants.StatusType.MovementAffecting) == true) {
            currentHorizontalDirection = 0f;
        }
        else {
            currentHorizontalDirection = Facing == FacingDirection.Left ? -1 : 1;
        }

        //Debug.Log("facing: " + Facing);
        //Debug.Log("dir: " + currentHorizontalDirection);


        //desiredSpeed = baseMoveSpeed;
        UpdateFacing();
    }

    protected override void UpdateFacing() {
        if (Owner == null)
            return;


        if (currentHorizontalDirection < 0 && Owner.SpriteRenderer.flipX == false) {
            if (defaultFacingLeft == false)
                Owner.SpriteRenderer.flipX = true;
            else
                Owner.SpriteRenderer.flipX = false;
            SwapWeaponSide();
        }

        if (currentHorizontalDirection > 0 && Owner.SpriteRenderer.flipX == true) {
            if (defaultFacingLeft == false)
                Owner.SpriteRenderer.flipX = false;
            else
                Owner.SpriteRenderer.flipX = true;
            SwapWeaponSide();
        }
    }



    public void FlipDirection() {
        //if (recentFlip == true)
        //    return;

        
        recentFlip = true;
        //pauseTurning = true;
        //turnPauseTimer.ResetTimer();
        Owner.SpriteRenderer.flipX = !Owner.SpriteRenderer.flipX;
    }

    private void ResetFlipTimer() {
        recentFlip = false;
    }

    //private void ResetPauseTimer() {
    //    turnPauseTimeCurrent = Random.Range((turnPauseTime / 2f), turnPauseTime);
    //    turnPauseTimer.SetNewDuration(turnPauseTimeCurrent);
    //    pauseTurning = false;
    //}


}
