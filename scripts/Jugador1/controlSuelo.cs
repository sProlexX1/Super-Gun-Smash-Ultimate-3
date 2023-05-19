using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlSuelo : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool enSuelo;

    [SerializeField] private bool enSueloInspector = enSuelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            enSuelo = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enSuelo = false;
    }
}
