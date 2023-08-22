using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;
using Mirror;
using System.Linq;

public class AgentMovement : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private int moveSpeed = 3;

    public Dictionary<WayType, GameObject> wayObjects = new();
    public WayType currnetWayType;

    private Vector2 inputVec { get; set; }
    private Vector2 checkVec { get; set; }

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = (Rigidbody2D)GetComponent("Rigidbody2D");
    }
    void Start()
    {
        foreach (WayType item in Enum.GetValues(typeof(WayType)))
        {
            GameObject obj = transform.Find(item.ToString()).gameObject;
            wayObjects.Add(item, obj);
        }

        _inputReader.MovementEvent += HandleMovement;
    }
    private void FixedUpdate()
    {
        //if (!isOwned) return;
        _rb.velocity = (inputVec * moveSpeed);
    }
    private void HandleMovement(Vector2 move)
    {
        Debug.Log(inputVec);
        inputVec = move;


        if (inputVec.x > 0) currnetWayType = WayType.Right;
        else if (inputVec.x < 0) currnetWayType = WayType.Left;
        else if (inputVec.y > 0) currnetWayType = WayType.Back;
        else if (inputVec.y < 0) currnetWayType = WayType.Front;
        else currnetWayType = WayType.Front;

        SetAnimation(inputVec);
    }
    private void SetAnimation(Vector2 dir)
    {
        if (checkVec == dir) return;
        checkVec = dir;

        wayObjects.Select(currnetWayType,
            p => p.SetActive(true),
            p => p.SetActive(false));
    }
    private void OnDestroy()
    {
        _inputReader.MovementEvent -= HandleMovement;
    }
}
