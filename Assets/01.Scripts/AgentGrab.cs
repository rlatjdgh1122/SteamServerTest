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
    [Header("��Ÿ")]
    [SerializeField] private Collider2D _coll;

    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnHandleFire()
    {
        Debug.Log("�߻�");

        var instance = Instantiate(_GrabObject, _pivot.position, Quaternion.identity);

        instance.SetDirection(dir);
        instance.SetThisCollider(_coll);

        instance.transform.up = _pivot.up;

        if (instance.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * Speed;
        }
    }
}
