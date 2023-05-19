using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempoDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float tiempoVida;

    private void Start()
    {
        Destroy(gameObject, tiempoVida);
    }


}
