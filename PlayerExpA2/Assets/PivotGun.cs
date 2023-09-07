using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PivotGun : MonoBehaviour
{
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private Vector3 pivotPoint;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartPivot();
        }
            else if (Input.GetMouseButtonUp(1))
        {
            StopPivot();
        }
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void StartPivot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, player.position, out hit,maxDistance, whatIsGrappleable)) 
        { 
            pivotPoint= hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = pivotPoint;

            float distanceFromPoint = Vector3.Distance(player.position, pivotPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2; 
        }
    }
    void StopPivot()
    {
        lr.positionCount = 0;
        Destroy (joint);
    }
    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(0, pivotPoint);

    }
}
