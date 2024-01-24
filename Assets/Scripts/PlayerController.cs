using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject canvasDead;
    [SerializeField] Animator CanvasAnimator;

    public Transform positionCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsDying()
    {
        anim.SetTrigger("Die");
        canvasDead.SetActive(true);
        StartCoroutine("WaitSomeSeconds");
        Debug.Log("Dead");
    }

    private IEnumerator WaitSomeSeconds()
    {
        player.enabled = false;
        yield return new WaitForSeconds(3f);
        player.transform.position = positionCheckpoint.position;
        player.enabled = true;
        CanvasAnimator.SetTrigger("Reborn");
        yield return new WaitForSeconds(2f);
        canvasDead.SetActive(false);


    }


    public void SetPositionCheckpoint(Transform pos)
    {
        positionCheckpoint = pos;
    }
}
