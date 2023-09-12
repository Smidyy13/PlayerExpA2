using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask grappleable;
    [SerializeField] float pullForce;
    [SerializeField] float pullAcceleration;
    [SerializeField] float grappleRange;

    Rigidbody rb;

    bool grappling;

    Vector3 grapplePointDirection;

    public bool canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        canMove = true;
    }


    void Update()
    {
        if (canMove)
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
            }
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


    void CastRay()
    {
        RaycastHit hit;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, grappleRange, grappleable))
        {
            grapplePointDirection = hit.point;
            grappling = true;
        }
    }

    public void Freeze()
    {
        rb.velocity = Vector3.zero;
    }

    public void HaltMovement()
    {
        canMove = false;
        grappling = false;
    }

    public void ContinueMovement()
    {
        canMove = true;
    }
}
