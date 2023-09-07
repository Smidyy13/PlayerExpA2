using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{

    [SerializeField] Camera camera;
    [SerializeField] LayerMask grappleable;
    [SerializeField] float pullForce;
    [SerializeField] float pullAcceleration;
    [SerializeField] float grappleRange;
    public LineRenderer lr;
    public Transform gunTip;

    Rigidbody rb;

    bool grappling;

    Vector3 grapplePointDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!grappling)
            {
                CastRay();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            grappling = false;
            lr.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (grappling)
        {
            float appliedPullForce = Mathf.Lerp(0, pullForce, pullAcceleration);

            rb.AddForce((grapplePointDirection - transform.position).normalized * appliedPullForce);
        }
    }

    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, gunTip.position);
    }
    void CastRay()
    {
        RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, grappleRange, grappleable))
        {
            grapplePointDirection = hit.point;
            grappling = true;
        }
        lr.enabled = true;
        lr.SetPosition(1, hit.point);
    }
}
