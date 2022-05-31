using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)     //Se utiliza cuando un colider entra en un trigger
    {
        if (collision.tag == "Player")
        {
            PlayerController controller = collision.GetComponent<PlayerController>(); //Recupero la componente playercontroller para poder indicar que debe morir

            controller.Die();
                
        }
    }
}
