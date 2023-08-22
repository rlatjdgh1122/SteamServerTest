using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;
using Mirror;
using System.Linq;

public class AgentMovement : NetworkBehaviour
{
    public int moveSpeed = 3;

    private Dictionary<WayType, GameObject> wayObjects = new();
    private WayType currnetWayType;

    private Vector2 direction { get; set; }
    private Vector2 inputVec { get; set; }
    void Start()
    {
        foreach (WayType item in Enum.GetValues(typeof(WayType)))
        {
            GameObject obj = transform.Find(item.ToString()).gameObject;
            wayObjects.Add(item, obj);
        }
    }

    void Update()
    {
        SetDirection();
    }
    private void SetDirection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            currnetWayType = WayType.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            currnetWayType = WayType.Right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
            currnetWayType = WayType.Back;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
            currnetWayType = WayType.Front;
        }
        else
        {
            currnetWayType = WayType.Front;
            return;
        }

        Move(direction);
        SetAnimation(direction);
    }

    private void Move(Vector2 dir)
    {
        Debug.Log("¿òÁ÷ÀÓ");
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    private void SetAnimation(Vector2 dir)
    {
        if (inputVec == dir) return;
        inputVec = dir;

        wayObjects.Select(currnetWayType,
            p => p.SetActive(true),
            p => p.SetActive(false));
    }
}
