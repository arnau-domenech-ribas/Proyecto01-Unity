using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2D : MonoBehaviour
{

    public float velocidad = 5f;

    void Start()
    {
            
    }

    void Update()
    {
            
        Vector3 movimiento = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position = transform.position + movimiento * velocidad * Time.deltaTime;
        Debug.Log(movimiento);

    }

}
