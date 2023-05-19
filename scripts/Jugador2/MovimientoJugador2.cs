using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador2 : MonoBehaviour
{

    Rigidbody2D rgbd2D;
    private Animator animator;

    // Start is called before the first frame update

    private float movimientoHorizontal2 = 0f;

    [SerializeField] private float velocidadDeMovimiento;

    [Range(0, 0.2f)][SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;

    //SALTO

    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;



    private void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        movimientoHorizontal2 = Input.GetAxisRaw("HorizontalJugador2") * velocidadDeMovimiento;

        animator.SetFloat("Horizontal2", Mathf.Abs(movimientoHorizontal2));

        if (movimientoHorizontal2 < 0.0f)//GIRAR DERECHA
        {
            transform.localScale = new Vector3(-1.52f, 1.8f, 1.0f);
            mirandoDerecha = true;

        }
        else if (movimientoHorizontal2 > 0.0f) //GIRAR IZQUIERDA
        {
            transform.localScale = new Vector3(1.52f, 1.8f, 1.0f);
            mirandoDerecha = false;
        }

        if (Input.GetKeyDown(KeyCode.I))//SALTAR
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo2", enSuelo);



        Mover(movimientoHorizontal2 * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rgbd2D.velocity.y);
        rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);


        if (enSuelo && saltar)
        {
            enSuelo = false;
            rgbd2D.AddForce(new Vector2(0f, fuerzaSalto));
        }
    }

    public void Empujar()
    {
        rgbd2D.AddForce(Vector2.right);
    }

    
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }


}
