using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSizeController : MonoBehaviour
{
    [SerializeField] float delta;
    [SerializeField] float minimum;
    [SerializeField] float maximum;
    [SerializeField] CinemachineVirtualCamera camara;

    private float xfactor;

    [SerializeField] float[] scales;


    // Start is called before the first frame update
    void Start()
    {
        xfactor = maximum;
        transform.localScale = new Vector2(minimum, minimum);
        camara = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseSize()
    {
        xfactor += delta;
        if (transform.localScale.x <= maximum)
        {
            transform.localScale = new Vector2(xfactor, xfactor);
            camara.m_Lens.OrthographicSize += 0.38f;


        }

    }
    public void DecreaseSize()
    {
        xfactor -= delta;
        if (transform.localScale.x >= minimum)
        {
            transform.localScale = new Vector2(xfactor, xfactor);
            camara.m_Lens.OrthographicSize -= 0.72f;

        }
        else
        {
            transform.localScale = new Vector2(minimum, minimum);
        }

    }

    public void ResizeFactor()
    {
        int Ypos = (int)gameObject.transform.position.y;

        float value = scales[Ypos];

        transform.localScale = new Vector2(value, value);
    }
}
