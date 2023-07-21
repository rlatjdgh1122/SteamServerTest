using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnMoveEvent = null;

    private Vector2 movePos;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        DontDestroyOnLoad(cam);
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
           movePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        OnMoveEvent?.Invoke(movePos);
    }
}
