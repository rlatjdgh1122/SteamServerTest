using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentGrab : NetworkBehaviour
{
    [SerializeField] private Grab _GrabObject;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float Speed;

    private void Start()
    {
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnHandleFire()
    {
        Debug.Log("버튼 눌림");
    }
}
