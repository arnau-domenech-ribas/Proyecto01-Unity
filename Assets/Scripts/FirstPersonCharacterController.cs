using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{

    [Header("Model")]
    public GameObject character;

    [Header("Movimiento")]
    public float velocidad;
    [Range(1,2)]
    public float groundCheck = 1.1f;
    bool grounded;
    public float jumpStrenght = 100;
    public float jetpackStrenght = 100;
    bool activeJetpack = false;
    float jumpCounter;
    bool dobleJump;
    float deposit = 1;
    public Rigidbody rb;

    [Header("Camara")]
    public Camera mainCamera;
    public float sensitividadVertical;
    public float sensitividadHorizontal;
    public float verticalMax;
    public float verticalMin;
    Vector3 rotacion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //detectar si estas en el suelo
        RaycastHit hit;   
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheck))
        {
            grounded = true;
            dobleJump = true;
            Debug.DrawRay(transform.position, -transform.up * groundCheck, Color.green);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(transform.position, -transform.up * groundCheck, Color.red);
        }
    }

    void Update()
    {
        //rotar personaje horizontalmente
        rotacion.y += Input.GetAxis("Mouse X") * sensitividadHorizontal * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotacion.y, 0);


        //mover personaje
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        float movimientoVertical = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;
        transform.position += transform.forward * movimientoVertical + transform.right * movimientoHorizontal;

        Jump();
    }

    void Jump()
    {
        //saltar
        if (Input.GetButtonDown("Jump") && grounded && jumpCounter <= 0)
        {
            jumpCounter = .25f;
            rb.AddForce(transform.up * jumpStrenght);
        }
        if (jumpCounter >= -1)
        {
            jumpCounter -= Time.deltaTime; //detiene el jumpCounter al llegar a -1
        }

        //doble salto
        if (Input.GetButtonDown("Jump") && !grounded && dobleJump)
        {
            dobleJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); //frena la caida antes del doble salto
            rb.AddForce(transform.up * jumpStrenght);
        }

        //jetpack (se activa despues del doble salto)
        if (grounded)
        {
            activeJetpack = false;
            deposit = Mathf.Clamp01(deposit + Time.deltaTime);
        }
        else
        {
            if(Input.GetButtonUp("Jump") && !dobleJump) //necesitas darle de nuevo al boton de salto
            {
                activeJetpack = true;
            }
            if (Input.GetButton("Jump") && deposit > 0 && activeJetpack)
            {
                deposit -= Time.deltaTime;
                rb.AddForce(transform.up * jetpackStrenght * Time.deltaTime);
            }
        }
    }

    void LateUpdate()
    {
        //rotar camara verticalmente
        rotacion.x = Mathf.Clamp(rotacion.x + Input.GetAxis("Mouse Y") * sensitividadVertical * Time.deltaTime, verticalMin, verticalMax);
        mainCamera.transform.localEulerAngles = new Vector3(-rotacion.x, 0, 0);
    }

}
