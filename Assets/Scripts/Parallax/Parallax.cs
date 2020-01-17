using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public Transform cameraTransform;
    public Vector2 parallaxEffectMultiplier;


    public float minDistance;

    private Vector3 lastCamPos;

    private bool inRange;

    private void Start() {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCamPos = cameraTransform.position;
    }

    private void Update() {
        if(minDistance != 0f)
            inRange = CheckMinDistance();
    }

    private void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCamPos;


        if (minDistance != 0f) {
            if(inRange)
                UpdatePosition(deltaMovement);
        }
        else {
            UpdatePosition(deltaMovement);
        }

        lastCamPos = cameraTransform.position;
    }


    private void UpdatePosition(Vector3 deltaMovement) {
        //Vector3 deltaMovement = cameraTransform.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);

    }

    private bool CheckMinDistance() {
        float distance = Vector2.Distance(cameraTransform.position, transform.position);

        return distance <= minDistance;
    }


}
