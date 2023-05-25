using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    //ESTE ES UN MENSAJE DE 19-05
    Rigidbody2D rgbd2D;
    private Animator animator;
    // Start is called before the first frame update

    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadDeMovimiento;

    [Range(0, 0.2f)][SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;

    public Vector2 input;

    //SALTO

    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    [SerializeField] private bool enFuerza;


    private bool salto = false;



    private void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("HorizontalJugador1");
        input.y = Input.GetAxisRaw("VerticalJugador1");

        //movimientoHorizontal = Input.GetAxisRaw("HorizontalJugador1") * velocidadDeMovimiento;
        movimientoHorizontal = input.x * velocidadDeMovimiento;

        animator.SetFloat("Horizontal1", Mathf.Abs(movimientoHorizontal));


        /*
        if (movimientoHorizontal < 0.0f)//GIRAR DERECHA
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }else if (movimientoHorizontal > 0.0f) //GIRAR IZquierda
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        */

        if (Input.GetKeyDown(KeyCode.W))//SALTAR
        {
            if (input.y >= 0)
            {
                salto = true;
            }else
            {
                DesactivarPlataformas();
            }
            
        }
    }

    //HOLAaa

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo1", enSuelo);
        
        
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void DesactivarPlataformas()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        foreach(Collider2D item in objetos)
        {
            PlatformEffector2D platformEffector2D =  item.GetComponent<PlatformEffector2D>();
            if(platformEffector2D != null)
            {
                //Physics2D para ignorar  colisiones, esta  toma 2 colliders,el primero es del  jugador y el segundo es
                //el objeto que tocamos. por ultimo el true es para que desactive la colision.
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
            }
        }
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rgbd2D.velocity.y);
        rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if(mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if(mover < 0 && mirandoDerecha)
        {
            Girar();
        }


        if(enSuelo && saltar)
        {
            enSuelo = false;
            rgbd2D.AddForce(new Vector2(0f, fuerzaSalto));
        }
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
