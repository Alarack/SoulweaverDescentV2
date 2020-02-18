using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAtLedgeAction : BaseStateAction {

    EnemyMovement enemyMovement;

    private Timer walkPauseTimer;
    private float walkPauseTimeCurrent;
    private bool pauseWalk;

    private bool pauseFlip;
    private Timer flipCooldown;

    public TurnAtLedgeAction(Entity owner, bool runUpdate) : base(owner, runUpdate)
    {
        enemyMovement = owner.Movement as EnemyMovement;

        walkPauseTimer = new Timer(enemyMovement.turnPauseTime, ResetPauseTimer, false);
        walkPauseTimeCurrent = enemyMovement.turnPauseTime;

        flipCooldown = new Timer(enemyMovement.turnPauseTime / 2f, ResetFlipTimer, true);
    }

    public override void Execute()
    {
        if (pauseWalk == true && walkPauseTimer != null) {
            walkPauseTimer.UpdateClock();
        }

        if (pauseFlip == true && flipCooldown != null) {
            flipCooldown.UpdateClock();
        }

        enemyMovement.pauseWalk = pauseWalk;

        if (pauseWalk == true || pauseFlip == true) {
            return;
        }

        if (enemyMovement.RayController.IsAtLedge)
        {
            pauseWalk = true;
            pauseFlip = true;
            walkPauseTimer.ResetTimer();
            flipCooldown.ResetTimer();
            //enemyMovement.FlipDirection();
            //Debug.Log("Edge flip");
        }
    }

    private void ResetPauseTimer() {
        walkPauseTimeCurrent = Random.Range((enemyMovement.turnPauseTime / 2f), enemyMovement.turnPauseTime);
        walkPauseTimer.SetNewDuration(walkPauseTimeCurrent);
        pauseWalk = false;

        
    }

    private void ResetFlipTimer() {
        pauseFlip = false;
        enemyMovement.FlipDirection();
    }

}
