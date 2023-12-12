using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    [SerializeField] LayerMask HookMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHanging())
        {
            Debug.Log("Engaged");
        }
    }
    private bool IsHanging()
    {
        return Physics2D.OverlapCircle(transform.position, 1f, HookMask);
        
    }
}
