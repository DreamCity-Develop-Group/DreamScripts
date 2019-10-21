using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public GetPath path;
    private Transform[] Path;
    public float MoveSpeed;
    private int Index = 0;

    private void Start()
    {
        Path = path.Path;
    }

    void Update()
    {
       
        transform.LookAt(Path[Index]);
       
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, Path[Index].position);
        if (distance < 5)
        {
            Index++;
            if(Index>= Path.Length)
            {
                Index = 0;
            }
        }
       
    }
}
