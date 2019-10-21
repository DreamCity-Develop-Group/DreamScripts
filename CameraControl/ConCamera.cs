using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitBenderGames;

public class ConCamera : MonoBehaviour
{
    private MobileTouchCamera TouchCamera;
    public Transform target;
    private float MoveSpeed = 200;
    private float distance = 0;
    private bool IsMove = true;
    private TouchInputController touch;
    public static bool IsActivateTouch = true;
    private bool IsSet = true;

    void Start()
    {
        TouchCamera = transform.GetComponent<MobileTouchCamera>();
        touch = transform.GetComponent<TouchInputController>();
        //TouchCamera.EnableRotation = true;
    }

    void Update()
    {
        if (IsMove)
        {
            MoveTo();
        }
        if(IsActivateTouch)
        {
            if (IsSet)
            {
                touch.enabled = true;
                IsSet = false;
            }      
        }
        else
        {
            if(!IsSet)
            {
                touch.enabled = false;
                IsSet = true;
            }
           
        }
    }
    private void MoveTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < 2)
        {
            IsMove = false;
            TouchCamera.EnableRotation = true;
        }
    }
}
