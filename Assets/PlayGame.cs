using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject CanvasTOEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        anim.SetTrigger("Play");
        StartCoroutine("CanvasStory");
        StartCoroutine("WaitAndDeactivate");
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(2f);
        canvas.enabled = false;
    }

    private IEnumerator CanvasStory()
    {
        yield return new WaitForSeconds(0.2f);
        CanvasTOEnable.gameObject.SetActive(true);
        Debug.Log("Deactivating");
    }

}
