using Unity.Cinemachine;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private Animator anim;


    // Variable del collider de la espada
    [SerializeField] private Collider ataque;

    //Variables camara
    [SerializeField] CinemachineCamera camara;
    //TRIGGER QUE ACTIVA LA CÁMARA VIRTUAL
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SensorCamara"))
        {
            camara.Priority = 1000;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SensorCamara"))
        {
            camara.Priority = -1;
        }
    }

    [Header("MOVIMIENTO")]
    [SerializeField] private float velocidad = 4.0f;
    [SerializeField] private float velocidadGiro = 360.0f;
    [SerializeField] private float fuerzaSalto = 5.0f;
    //Raycast para salto
    private Ray rayo;
    private RaycastHit impactoRayo;
    [SerializeField] float longitudRayo = 0.4f;

    // COGEMOS LOS COMPONENTES
    private void Awake()
    {
        rb = GetComponent <Rigidbody>();
        anim = GetComponent <Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        // Definimos el origen y la direccion del rayo
        rayo.origin = transform.position + (new Vector3(0.0f, 0.2f, 0.0f));
        rayo.direction = -transform.up;

        // Activar animacion de movimiento salto
        if (Physics.Raycast(rayo, out impactoRayo, longitudRayo) && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

        Debug.DrawRay(rayo.origin, rayo.direction * longitudRayo * 10, Color.blue);
        // Activar la animación de movimiento ataque
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }
    private void FixedUpdate()
    {
        // Controles de teclado
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Movimientos en cordenadas locales
        //                  (              SUMA DE VECTORES             ).NORMALIZED
        Vector3 direccion = (transform.right * h + transform.forward * v).normalized;
        Vector3 movimiento = direccion * velocidad * Time.fixedDeltaTime;

        // Aplicamos el metodo MovePosition (donde que vector que posicion)
        rb.MovePosition(rb.position + movimiento);

        // Activar la animacion de movimiento forward
        if (v != 0.0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (h != 0.0f)
        {
            if (h < 0.0f)
            {
                anim.SetBool("isMovingLFT", true);
                anim.SetBool("isMovingRGT", false);
            }
            else
            {
                anim.SetBool("isMovingRGT", true);
                anim.SetBool("isMovingLFT", false);
            }
        }
        else
        {
            anim.SetBool("isMovingLFT", false);
            anim.SetBool("isMovingRGT", false);
        }

        //Control de ratón
        float giro = Input.GetAxis("Mouse X");

        //Giro en coordenadas locales
        float giroY = giro * velocidadGiro * Time.fixedDeltaTime;
        Quaternion rotacion = Quaternion.Euler(0.0f, giroY, 0.0f);
        //Aplicamos el método Move Rotation (dónde que giro hacer- Ojo, se MULTIPLICA)
        rb.MoveRotation(rb.rotation * rotacion);
    }
    // HABILITAR COLIDER
    private void Habilitar()
    {
        ataque.enabled = true;
    }
    // DESHABILITAR COLIDER
    private void Dshabilitar()
    {
        ataque.enabled = false;
    }
}
