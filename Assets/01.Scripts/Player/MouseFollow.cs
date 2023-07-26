using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private float offset = -.8f;
    private Vector3 tempPos;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);
    }
    private void Update()
    {
        tempPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10));
        transform.position = new Vector3(tempPos.x, offset, tempPos.z);
    }
}
