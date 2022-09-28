using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destruible : MonoBehaviour
{

    public UnityEvent alEjecutar;

    public void Ejecutar()
    {
        alEjecutar.Invoke();
    }

}
