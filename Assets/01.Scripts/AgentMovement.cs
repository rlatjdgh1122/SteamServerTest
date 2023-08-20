using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

public class AgentMovement : MonoBehaviour
{
    private Dictionary<WayType, GameObject> wayObjects = new();
    private WayType currnetWayType;
    public int moveSpeed = 3;
    void Start()
    {
        foreach (WayType item in Enum.GetValues(typeof(WayType)))
        {
            GameObject obj = transform.Find(item.ToString()).gameObject;
            wayObjects.Add(item,obj);
        }
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 vec = new Vector2(inputX, inputY);
        if(vec.x > 0)
        {
            currnetWayType = WayType.Right;
        }
        else if(vec.x < 0)
        {
            currnetWayType = WayType.Left;
        }
        else if(vec.y > 0)
        {
            currnetWayType = WayType.Back;
        }
        else if(vec.y < 0)
        {
            currnetWayType = WayType.Front;
        }

        transform.Translate(vec * moveSpeed * Time.deltaTime);

    }
}
