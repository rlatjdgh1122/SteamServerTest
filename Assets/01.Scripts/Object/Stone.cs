using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject mousePointA;
    public GameObject mousePointB;
    public GameObject arrow;
    public GameObject circle;
    

    public float maxDistance = 3f;
    public float shootPower = 10;

    private float offset = 10.1f;
    private float power;
    private float currentDistance;
    private float safeSpace;
    private float difference;
    private float rot;

    private Vector3 shootDirection;
    private Vector3 dir;

    private Renderer arrowRen;
    private Renderer circleRen;

    private Rigidbody rb;
    private void Awake()
    {
        rb = (Rigidbody)GetComponent("Rigidbody");

        arrowRen = (Renderer)arrow.GetComponent("Renderer");
        circleRen = (Renderer)circle.GetComponent("Renderer");

        arrowRen.enabled = false;
        circleRen.enabled = false;
    }
    private void OnMouseDrag()
    {
        Debug.Log("마우스 드래그");

        currentDistance = Vector3.Distance(
            new Vector3(mousePointA.transform.position.x, 0, mousePointA.transform.position.z),
            new Vector3(transform.transform.position.x, 0, transform.transform.position.z)
            );
        Debug.Log("현재 거리 : " + currentDistance);
        if (currentDistance <= maxDistance)
        {
            safeSpace = currentDistance;
        }
        else
            safeSpace = maxDistance;

        doArrowAndCircleStuff();

        dir = mousePointA.transform.position - transform.position;
        difference = dir.magnitude;
        Debug.Log("거리 : " + safeSpace);

        power = Mathf.Abs(safeSpace) * shootPower;
        //power = Mathf.Abs(safeSpace) * 10;

        mousePointB.transform.position = transform.position + ((dir / difference) * currentDistance * -1f);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, offset, mousePointB.transform.position.z);


        shootDirection = Vector3.Normalize(new Vector3(mousePointA.transform.position.x - transform.position.x, 0, mousePointA.transform.position.z - transform.position.z));
        //shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }


    private void OnMouseUp()
    {
        arrowRen.enabled = false;
        circleRen.enabled = false;

        Vector3 push = shootDirection * power * -1f;
        Debug.Log("shootDirection : " + shootDirection.magnitude);
        Debug.Log("힘 : " + push.magnitude);

        rb.AddForce(push, ForceMode.Impulse);
    }
    private void doArrowAndCircleStuff()
    {
        arrowRen.enabled = true;
        circleRen.enabled = true;

        dir = mousePointA.transform.position - transform.position;
        difference = dir.magnitude;

        if (currentDistance <= maxDistance)
        {
            arrow.transform.position = new Vector3(
                (2 * transform.position.x) - mousePointA.transform.position.x,
                offset,
                (2 * transform.position.z) - mousePointA.transform.position.z);
        }
        else
        {
            arrow.transform.position = transform.position + ((dir / difference * maxDistance * -1f));
            arrow.transform.position = new Vector3(
                arrow.transform.position.x,
                offset,
                arrow.transform.position.z);
        }

        circle.transform.position = transform.position + new Vector3(0, 0, 0.04f);
        if (Vector3.Angle(dir, transform.forward) > 90)
            rot = Vector3.Angle(dir, transform.right);
        else
            rot = Vector3.Angle(dir, transform.right) * -1f;

        arrow.transform.eulerAngles = new Vector3(0, rot, 0);

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.4f;
        float scaleZ = Mathf.Log(1 + safeSpace / 2, 2) * 2.4f;

        arrow.transform.localScale = new Vector3(scaleX * .5f, 0.001f, scaleZ * .5f);
        circle.transform.localScale = new Vector3(scaleX, 0.001f, scaleZ);
    }
}
