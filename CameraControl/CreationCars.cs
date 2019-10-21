using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationCars : MonoBehaviour
{
    private GameObject[] cars;
    private int index = 0;
    private void Start()
    {
        cars = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            cars[i] = transform.GetChild(i).gameObject;
        }
        StartCoroutine(ActivateTheCar());
    }
    private IEnumerator ActivateTheCar()
    {
        do
        {
            cars[index].SetActive(true);
            yield return new WaitForSeconds(10f);
            index++;
        }
        while (index < transform.childCount);
    }
}
