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
    [SerializeField] private LineRenderer _lr;

    private Vector3 dir = Vector3.zero;

    private void Awake()
    {
        _lr.startWidth = .2f;
        _lr.endWidth = .2f;

        _lr.SetPosition(0, _pivot.position);
        _lr.positionCount = 2;

        _lr.enabled = false;
    }
    private void Start()
    {
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnHandleFire()
    {
        var instance = Instantiate(_GrabObject, _pivot.position, Quaternion.identity);

        Debug.Log("player��: " + (Vector2)_pivot.position);
        instance.SetThisCollider(_coll);

        instance.transform.up = _pivot.up;

        if (instance.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * Speed;

            instance.SetDirection(rb.transform.up);

        }
    }
}
