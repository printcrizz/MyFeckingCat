using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerEnding : MonoBehaviour
{
    [SerializeField] GameObject animCanvas;
    [SerializeField] Animator AnimCat;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public float initialOrthographicSize;  
    public float targetOrthographicSize;

    public Transform target;  
    public float duration = 5f;  
    public float updateInterval = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private IEnumerator MoveCamera()
    {
        virtualCamera.Follow = target;
        initialOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, t);

            yield return new WaitForSeconds(updateInterval);

            elapsedTime += updateInterval;
        }

        virtualCamera.m_Lens.OrthographicSize = targetOrthographicSize;

    }

    public void ActivateCanvas()
    {
        animCanvas.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine("SetEnding");
            
        }
    }

    private IEnumerator SetEnding()
    {
        StartCoroutine(MoveCamera());

        yield return new WaitForSeconds(2f);

        AnimCat.SetTrigger("Meow");

        yield return new WaitForSeconds(3f);

        ActivateCanvas();

    }
}
