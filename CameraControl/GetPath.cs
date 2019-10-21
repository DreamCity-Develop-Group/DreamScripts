using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    public Transform[] Path;
    private void Start()
    {
        Path = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Path[i] = transform.GetChild(i);
        }
    }

}
