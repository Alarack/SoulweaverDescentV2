using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnchorManager : MonoBehaviour
{

    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;



    private void Start() {
        SetCamBounds();
    }

    public void SetCamBounds() {
        if(left == null || right == null || bottom == null || top == null) {
            Debug.LogError("[Cam Amchor Manager] One of the camera anchor points is null. Assign it in the insepctor.");
            return;
        }

        Vector2 leftPos = new Vector2(left.position.x + 9, left.position.y);
        Vector2 topPos = new Vector2(top.position.x, top.position.y - 5);
        Vector2 rightPos = new Vector2(right.position.x - 9, right.position.y);
        Vector2 bottomPos = new Vector2(bottom.position.x, bottom.position.y + 5);

        GameManager.CameraFollow.SetCameraBounds(leftPos, rightPos, bottomPos, topPos/*left.position, right.position, bottom.position, top.position*/);
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.8f);

        if (left != null)
            Gizmos.DrawCube(left.position, new Vector2(0.25f, 10));
        if(right != null)
            Gizmos.DrawCube(right.position, new Vector2(0.25f, 10));
        if(top != null)
            Gizmos.DrawCube(top.position, new Vector2(18, 0.25f));
        if(bottom != null)
            Gizmos.DrawCube(bottom.position, new Vector2(18, 0.25f));

    }


}
