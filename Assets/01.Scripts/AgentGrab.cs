using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentGrab : NetworkBehaviour
{
    [SerializeField] private Grab _GrabObject;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float Speed;
    [SerializeField] private CameraController cameraController;

    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        _inputReader.FireEvent += OnHandleFire;
    }
    private void Update()
    {
        Vector3 pos = cameraController.Cam.ScreenToWorldPoint(_inputReader.MousePostion);
        dir = (pos - transform.position).normalized;
    }

    private void OnHandleFire()
    {
        Debug.Log("버튼눌림");

        GameObject obj = Instantiate(_GrabObject.gameObject, transform.position, Quaternion.identity);
        obj.transform.up = dir;

        if (obj.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * Speed;
        }
    }
}
