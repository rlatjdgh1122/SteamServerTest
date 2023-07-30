using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
public class Stone : MonoBehaviour
{
    public GameObject arrow;
    public GameObject circle;

    private GameObject mousePointB;
    private GameObject mousePointA;

    public float maxDistance = 3f;
    public float shootPower = 10;

    private float offset = 10.1f;
    private float power;
    private float currentDistance;
    private float safeSpace;
    private float difference;
    private float rot;

    private Vector3 shootDirection;
    private Vector3 sub;

    private Renderer arrowRen;
    private Renderer circleRen;

    private Rigidbody rb;
    private void Awake()
    {
        rb = (Rigidbody)GetComponent("Rigidbody");

        arrowRen = (Renderer)arrow.GetComponent("Renderer");
        circleRen = (Renderer)circle.GetComponent("Renderer");

        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");

        arrowRen.enabled = false;
        circleRen.enabled = false;
    }
    private void OnMouseDrag()
    {
        Debug.Log("마우스 드래그");

        currentDistance = Vector3.Distance(
            Calculate.Get_in_plane_Vector(mousePointA.transform.position),
           Calculate.Get_in_plane_Vector(transform.position)
            );

        if (currentDistance <= maxDistance)
        {
            safeSpace = currentDistance;
        }
        else
            safeSpace = maxDistance;

        doArrowAndCircleStuff();

        sub = Calculate.Get_in_plane_Subtraction(mousePointA.transform.position, transform.position);
        difference = sub.magnitude;

        power = Mathf.Abs(safeSpace) * shootPower;

        mousePointB.transform.position = transform.position + ((sub / difference) * currentDistance * -1f);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, offset, mousePointB.transform.position.z);


        /*shootDirection = Vector3.Normalize(new Vector3(mousePointA.transform.position.x - transform.position.x, 0, mousePointA.transform.position.z - transform.position.z));*/
        shootDirection = Calculate.Get_in_plane_Direction(mousePointA.transform.position,transform.position);
    }


    private void OnMouseUp()
    {
        arrowRen.enabled = false;
        circleRen.enabled = false;

        Vector3 push = shootDirection * power * -1f;

        rb.AddForce(push, ForceMode.Impulse);
    }
    private void doArrowAndCircleStuff()
    {
        arrowRen.enabled = true;
        circleRen.enabled = true;

        difference = sub.magnitude;

        /*sub = new Vector3(mousePointA.transform.position.x, 0, mousePointA.transform.position.z) - transform.position;*/
        sub = Calculate.Get_in_plane_Subtraction(mousePointA.transform.position,transform.position);
        if (currentDistance <= maxDistance)
        {
            arrow.transform.position = new Vector3(
                (2 * transform.position.x) - mousePointA.transform.position.x,
                offset,
                (2 * transform.position.z) - mousePointA.transform.position.z);
        }
        else
        {
            arrow.transform.position = transform.position + ((sub / difference * maxDistance * -1f));
            arrow.transform.position = new Vector3(
                arrow.transform.position.x,
                offset,
                arrow.transform.position.z);
        }
        circle.transform.position = transform.position + new Vector3(0, 0.04f, 0);
        if (Vector3.Angle(sub, transform.forward) > 90)
            rot = Vector3.Angle(sub, transform.right);
        else
            rot = Vector3.Angle(sub, transform.right) * -1f;

        arrow.transform.eulerAngles = new Vector3(0, rot, 0);

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.4f;
        float scaleZ = Mathf.Log(1 + safeSpace / 2, 2) * 2.4f;

        arrow.transform.localScale = new Vector3(scaleX, 0.001f, scaleZ);
        circle.transform.localScale = new Vector3(scaleX, 0.001f, scaleZ);
    }
}
