using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikesBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //gm animar al canvas de muerte

            //animar al player llamando funcion muerte
            player.IsDying();


        }


    }
}
