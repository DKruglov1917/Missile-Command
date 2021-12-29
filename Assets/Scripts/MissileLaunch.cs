using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunch : MonoBehaviour
{
    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType missileType;
    [SerializeField]
    private Vector3 spawnPosition;

    public delegate void shoot();
    public static event shoot onPlayerShoot;

    public static int whichCity;
    private bool bLaunch;

    public void Launch()
    {
        LocateStartPosition();

        var missile = ObjectPooler.Instance.GetObject(missileType);
        missile.GetComponent<Missile>().OnCreate(spawnPosition, transform.rotation);
    }

    private void Update()
    {
        if (!GameController.bEnd)
        {
            if (missileType == ObjectPooler.ObjectInfo.ObjectType.PLAYER_MISSILE)
            {
                if (Input.GetMouseButtonDown(0) && Ammunition.curAmmo > 0 && !Menu.bActive)
                {
                    Launch();
                    onPlayerShoot?.Invoke();
                }
            }
            else
            {
                StartCoroutine("EnemyLaunch");
            }
        }
    }

    private void LocateStartPosition()
    {
        switch (missileType)
        {
            case ObjectPooler.ObjectInfo.ObjectType.PLAYER_MISSILE:
                spawnPosition = gameObject.transform.position;
                break;
            case ObjectPooler.ObjectInfo.ObjectType.ENEMY_MISSILE:
                whichCity = Random.Range(0, Cities.valCities);
                spawnPosition = new Vector3(GameObject.Find("Cities").transform.GetChild(whichCity).gameObject.transform.position.x, 10);
                break;
            case ObjectPooler.ObjectInfo.ObjectType.ENEMY_CRUISE_MISSILE:
                spawnPosition = new Vector3(Random.Range(-13, 14), 5);
                break;
        }
    }

    IEnumerator EnemyLaunch()
    {
        if (!bLaunch)
        {
            bLaunch = true;
            Launch();
            yield return new WaitForSeconds(Random.Range(1, 5 * GameController.gameSpeedMultiplier));
            bLaunch = false;
        }
    }
}
