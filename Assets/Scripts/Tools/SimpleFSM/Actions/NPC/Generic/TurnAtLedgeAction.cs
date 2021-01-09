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

    private Vector2? lastEdgeLocation;


    public TurnAtLedgeAction(Entity owner, bool runUpdate) : base(owner, runUpdate) {
        enemyMovement = owner.Movement as EnemyMovement;

        walkPauseTimer = new Timer(enemyMovement.turnPauseTime, ResetPauseTimer, false);
        walkPauseTimeCurrent = enemyMovement.turnPauseTime;

        lastEdgeLocation = null;
        //flipCooldown = new Timer(enemyMovement.turnPauseTime / 2f, ResetFlipTimer, true);
    }

    public override void Execute() {
        if (pauseWalk == true && walkPauseTimer != null) {
            walkPauseTimer.UpdateClock();
        }

        //if (pauseFlip == true && flipCooldown != null) {
        //    flipCooldown.UpdateClock();
        //}

        enemyMovement.pauseWalk = pauseWalk;

        if (pauseWalk == true /*|| pauseFlip == true*/) {
            return;
        }

        //if (lastEdgeLocation == null) {
        //    lastEdgeLocation = enemyMovement.Owner.transform.position;
        //}
        //else {
        if (lastEdgeLocation != null) {
            float distance = Vector2.Distance(enemyMovement.Owner.transform.position, (Vector2)lastEdgeLocation);

            if (distance <= 1f)
                return;
            else
                lastEdgeLocation = null;

        }


        if (enemyMovement.RayController.IsAtLedge) {

            if(lastEdgeLocation == null)
                lastEdgeLocation = enemyMovement.Owner.transform.position;


            pauseWalk = true;

            //pauseFlip = true;
            walkPauseTimer.ResetTimer();
            //flipCooldown.ResetTimer();
            //enemyMovement.FlipDirection();
            //Debug.Log("Edge flip");
        }
    }

    private void ResetPauseTimer() {

        enemyMovement.FlipDirection();
        walkPauseTimeCurrent = Random.Range((enemyMovement.turnPauseTime / 2f), enemyMovement.turnPauseTime);
        walkPauseTimer.SetNewDuration(walkPauseTimeCurrent);
        pauseWalk = false;
        //enemyMovement.StartCoroutine(WalkAgain());


    }

    private IEnumerator WalkAgain() {
        WaitForSeconds waiter = new WaitForSeconds(0.1f);

        yield return waiter;
        pauseWalk = false;
    }

    private void ResetFlipTimer() {
        pauseFlip = false;
        //enemyMovement.FlipDirection();
    }

}
