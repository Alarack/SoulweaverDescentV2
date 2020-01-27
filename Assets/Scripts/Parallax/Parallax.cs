using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public Transform cameraTransform;
    public Vector2 parallaxEffectMultiplier;


    public float minDistance;
    public float maxDistance;

    private Vector3 lastCamPos;

    private bool inRange;
    private bool outOfRange;

    private Vector3 startPos;

    private void Start() {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCamPos = cameraTransform.position;
        startPos = transform.position;
    }

    private void Update() {
        if (minDistance != 0f)
            inRange = CheckMinDistance();

        if (maxDistance > 0f)
            outOfRange = CheckMaxDistance();
    }

    private void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCamPos;


        if (minDistance != 0f) {
            if (inRange)
                UpdatePosition(deltaMovement);
        }
        else {
            UpdatePosition(deltaMovement);
        }

        lastCamPos = cameraTransform.position;
    }


    private void UpdatePosition(Vector3 deltaMovement) {
        //Vector3 deltaMovement = cameraTransform.position - lastCamPos;
        Vector3 newPos = new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);

        if (outOfRange == true) {
            float newDist = Vector2.Distance((transform.position + deltaMovement), startPos);
            float oldDist = Vector2.Distance(transform.position, startPos);

            if (newDist > oldDist)
                return;
        }

        transform.position += newPos;

    }

    private bool CheckMinDistance() {
        float distance = Vector2.Distance(cameraTransform.position, transform.position);

        return distance <= minDistance;
    }
    private bool CheckMaxDistance() {
        float distance = Vector2.Distance(startPos, transform.position);

        return distance >= maxDistance;
    }


}
