using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cities : MonoBehaviour
{
    public static int valCities;
    private int maxCities;

    private void Start()
    {
        maxCities = gameObject.transform.childCount;
        valCities = maxCities;
    }

    private void Update()
    {
        valCities = gameObject.transform.childCount;
    }
}
