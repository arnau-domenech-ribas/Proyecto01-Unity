using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycast : MonoBehaviour
{

    public string tagName = "Enemy";

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.tag);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            if (hit.collider.tag == tagName)
            {
                hit.collider.GetComponent<Destruible>().Ejecutar();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        }
    }

}