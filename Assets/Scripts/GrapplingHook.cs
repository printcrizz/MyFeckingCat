using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _Distancejoint;
    //public SpringJoint2D _Distancejoint;
    public Rigidbody2D rb;
    public float force;
    private Vector3 MouseDir;
    public Transform LinePosition;
    public bool isGrappling;
    public Transform lookToHook;

    public bool canHook;


    // Start is called before the first frame update
    void Start()
    {
        isGrappling = false;
        _Distancejoint.autoConfigureDistance = true;
        //_Distancejoint.autoConfigureConnectedAnchor = true;
        _Distancejoint.enabled = false;
        _lineRenderer.enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (isGrappling && canHook)
        {

            if (Input.GetKeyDown(KeyCode.L))
            {

                Vector2 hookPos = lookToHook.transform.position;

                _lineRenderer.SetPosition(0, hookPos);
                _lineRenderer.SetPosition(1, transform.position);
                _Distancejoint.connectedAnchor = hookPos;
                _Distancejoint.enabled = true;
                LinePosition.position = hookPos;


            }
            if (Input.GetKey(KeyCode.L))
            {
                _lineRenderer.SetPosition(1, transform.position);
                _lineRenderer.enabled = true;
                //rb.gravityScale = 0.5f;

            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                _Distancejoint.enabled = false;

                _lineRenderer.enabled = false;
                //rb.gravityScale = 1.2f;

            }
            if (_Distancejoint.enabled)
            {
                _lineRenderer.SetPosition(1, transform.position);
            }
        }
        else
        {
            _Distancejoint.enabled = false;

            _lineRenderer.enabled = false;
        }

    }
    public void ActivateCanHook()
    {
        isGrappling = true; 
    }
}
