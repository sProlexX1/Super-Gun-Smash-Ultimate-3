using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject Bala;

    private void Update()
    {
        if (Input.GetButtonDown("FireJ1"))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        Instantiate(Bala, controladorDisparo.position, controladorDisparo.rotation);
    }
}
