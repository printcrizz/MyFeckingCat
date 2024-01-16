using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointConnector : MonoBehaviour
{
    public string tagname;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag==player.gameObject.tag)
        {
            player.GetComponent<GrapplingHook>().canHook = true;
            player.GetComponent<GrapplingHook>().lookToHook = transform;
            player.GetComponent<DistanceJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player.gameObject.tag)
        {
            collision.GetComponent<GrapplingHook>().canHook = false;
            player.GetComponent<GrapplingHook>().lookToHook = null;
            player.GetComponent<DistanceJoint2D>().connectedBody = null;


        }
    }
}
