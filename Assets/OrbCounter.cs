using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrbCounter : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] bool isActive;
    [SerializeField] ParticleSystem particles;

    [SerializeField] UnityEvent eventToTrigger;

    private void Start()
    {
        isActive = true;
        gm = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            isActive = false;
            gm.OrbCounter++;
            gm.GetComponent<CameraSizeController>().IncreaseSize();
            
            //eventToTrigger.Invoke();
            //activar la animacion y sonido
            //var emission = particles.emission;
            //emission.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
