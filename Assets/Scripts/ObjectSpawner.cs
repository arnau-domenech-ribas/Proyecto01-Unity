using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float despawn = 1000000;
    public GameObject objeto;

    public void SpawnObject()
    {
        GameObject temp = Instantiate(objeto, transform.position, transform.rotation);
        Destroy(temp, despawn);
    }

}
