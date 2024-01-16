using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStory : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    void Start()
    {
        //canvas.enabled = false;
    }

    public void ActivateCanvas()
    {
        canvas.enabled = true;
    }


}
