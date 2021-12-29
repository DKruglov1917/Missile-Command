using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IPooledObject
{
    public ObjectPooler.ObjectInfo.ObjectType Type => type;
    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType type;

    public delegate void destroy();
    public static event destroy onEnemyMissileDestroy, onEnemyCruiseMissileDestroy, onCityDestroy;

    public GameObject CityExplosion, MissileExplosion, CruiseMissileExplosion, MissileMissedExplosion;
    public GameObject TargetCity;
    private GameObject CitiesObj;

    public Vector3 target;

    private float lifeTime, currentLifeTime, speed;

    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        currentLifeTime = lifeTime;

        LocateTarget();
    }

    private void Awake()
    {
        CitiesObj = GameObject.Find("Cities");
        lifeTime = 5;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);

        if (transform.position == target)
        {
            Instantiate(MissileMissedExplosion, transform.position, Quaternion.identity);
            ObjectPooler.Instance.DestroyObject(gameObject);
        }

        if ((currentLifeTime -= Time.deltaTime) < 0)
            ObjectPooler.Instance.DestroyObject(gameObject);
    }

    public void LocateTarget()
    {
        switch (type)
        {
            case ObjectPooler.ObjectInfo.ObjectType.PLAYER_MISSILE:
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                speed = 1;
                break;
            case ObjectPooler.ObjectInfo.ObjectType.ENEMY_MISSILE:
                speed = .1f;
                TargetCity = CitiesObj.transform.GetChild(MissileLaunch.whichCity).gameObject;
                target = TargetCity.transform.position;
                break;
            case ObjectPooler.ObjectInfo.ObjectType.ENEMY_CRUISE_MISSILE:
                speed = .1f;
                TargetCity = CitiesObj.transform.GetChild(Random.Range(0, Cities.valCities)).gameObject;
                target = TargetCity.transform.position;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (type == ObjectPooler.ObjectInfo.ObjectType.PLAYER_MISSILE)
        {
            if (col.tag == "EnemyMissile")
            {
                Instantiate(MissileExplosion, col.transform.position, Quaternion.identity);
                col.transform.gameObject.SetActive(false);
                onEnemyMissileDestroy?.Invoke();
            }
            else if (col.tag == "EnemyCruiseMissile")
            {
                Instantiate(CruiseMissileExplosion, col.transform.position, Quaternion.identity);
                col.transform.gameObject.SetActive(false);
                onEnemyCruiseMissileDestroy?.Invoke();
            }
        }
        else if (type == ObjectPooler.ObjectInfo.ObjectType.ENEMY_MISSILE ||
                 type == ObjectPooler.ObjectInfo.ObjectType.ENEMY_CRUISE_MISSILE)
        {
            if (col.gameObject == TargetCity)
            {
                Instantiate(CityExplosion, col.transform.position, Quaternion.identity);
                Destroy(col.transform.gameObject);
                onCityDestroy?.Invoke();
            }
        }
    }
}
