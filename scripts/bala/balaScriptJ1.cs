using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaScriptJ1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float velocidad;



    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * velocidad * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player2"))
        {
            Destroy(gameObject);
            Debug.Log("La bala impacto al jugador 2");
            
        }
    }

}
