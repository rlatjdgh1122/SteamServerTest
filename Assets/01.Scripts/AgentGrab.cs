using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentGrab : NetworkBehaviour
{
    [Header("����")]
    [SerializeField] private float Speed;
    [SerializeField] private Transform _pivot;
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Grab _GrabObject;

    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnHandleFire()
    {
        Debug.Log("��ư����");

        var obj = Instantiate(_GrabObject.gameObject, _pivot.position, Quaternion.identity);
        Debug.Log((Vector2)dir);

        obj.transform.up = _pivot.up;

        if (obj.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * Speed;
        }
    }
}
