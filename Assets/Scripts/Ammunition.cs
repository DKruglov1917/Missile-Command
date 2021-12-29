using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public static int maxAmmo, curAmmo;
    private bool bAmmo;

    private void Awake()
    {
        MissileLaunch.onPlayerShoot += SpendAmmo;
    }

    private void Start()
    {
        maxAmmo = 6;
        curAmmo = maxAmmo;
    }

    private void Update()
    {
        StartCoroutine("ReplenishAmmo");
    }

    private void SpendAmmo()
    {
        curAmmo -= 1;
    }

    private void OnDisable()
    {
        MissileLaunch.onPlayerShoot -= SpendAmmo;
    }

    IEnumerator ReplenishAmmo()
    {
        if (!bAmmo && curAmmo < maxAmmo && !GameController.bEnd)
        {
            bAmmo = true;
            yield return new WaitForSeconds(1);
            curAmmo += 1;
            bAmmo = false;
        }
    }
}
