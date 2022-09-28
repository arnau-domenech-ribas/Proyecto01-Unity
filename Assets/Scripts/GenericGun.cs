using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGun : MonoBehaviour
{

    public int clipAmmoCount;
    public int clipAmmoMax;
    public int inventoryAmmoCount;
    public int inventoryAmmoMax;
    public float reloadTime = 0.5f;
    bool reloading;
    public bool automatic;
    public float firingSpeed;
    public float lastBullet;

    void Start()
    {

    }

    void Update()
    {
        if (automatic)
        {
            if (Input.GetButton("Fire1") && !reloading)
            {
                FireBullet();
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !reloading)
            {
                FireBullet();
            }
        }

        lastBullet += Time.deltaTime;

        if (Input.GetButtonDown("Reload") && !reloading && clipAmmoCount != clipAmmoMax)
        {
            StartCoroutine(ReloadCorutine());
        }

    }

    void FireBullet()
    {
        if (clipAmmoCount > 0 && lastBullet >= firingSpeed)
        {
            lastBullet = 0;
            clipAmmoCount--;
            Debug.Log("He disparado");
        }
    }

    IEnumerator ReloadCorutine()
    {
        reloading = true;

        Debug.Log("Recargar");

        yield return new WaitForSeconds(reloadTime);

        inventoryAmmoCount += clipAmmoCount;
        clipAmmoCount = Mathf.Clamp(inventoryAmmoCount, 0, clipAmmoMax);
        inventoryAmmoCount -= clipAmmoCount;

        reloading = false;
    }

}