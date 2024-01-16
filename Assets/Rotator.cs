using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject pivot;
    [SerializeField] bool IAmAttached;
    // Start is called before the first frame update
    void Start()
    {
        //IAmAttached = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IAmAttached)
        {
            //pivot.transform.position.E(new Vector3(0,0,1),0.5f);
        }
    }
}
