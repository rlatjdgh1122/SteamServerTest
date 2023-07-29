using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float offset = 10.1f;
    private Camera cam;
    private Vector3 tempPos;

    private Ray ray;
    private RaycastHit hit;

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
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
            tempPos = hit.point;

        transform.position = new Vector3(tempPos.x, offset, tempPos.z);
    }
}
