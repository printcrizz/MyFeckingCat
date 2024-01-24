using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateFunctionality : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    public bool isActive;

    [SerializeField] Animator anim;

    [SerializeField] UnityEvent eventToTrigger;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                eventToTrigger.Invoke();
                callAnimationAndDisable();
            }
        }
        
    }

    private void callAnimationAndDisable()
    {
        anim.SetTrigger("Activation");
        StartCoroutine("WaitAndDeactivate");
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(2f);
        isActive = false;
    }
}
