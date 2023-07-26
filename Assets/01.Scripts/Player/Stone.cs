using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private GameObject mousePointA;
    private GameObject mousePointB;
    private GameObject arrow;
    private GameObject cicle;

    public float maxDistance = 3f;
    private float currentDistance;
    private float safeSpace;
    private float shootPower;

    private Vector3 shootDirection;

    private Rigidbody rb;
    private void Awake()
    {
        rb =(Rigidbody)GetComponent("Rigidbody"); 


        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
    }
    private void OnMouseDrag()
    {
        currentDistance = Vector3.Distance(mousePointA.transform.position, transform.position);

        if (currentDistance <= maxDistance)
        {
            safeSpace = currentDistance;
        }
        else
            safeSpace = maxDistance;

        shootPower = Mathf.Abs(safeSpace) * 10;

        Vector3 dimxy = mousePointA.transform.position - transform.position;
        float difference = dimxy.magnitude;
        mousePointB.transform.position = transform.position + ((dimxy / difference) * currentDistance * -1f);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, -.8f, mousePointB.transform.position.z);

        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }
    private void OnMouseUp()
    {
        Vector3 push = shootDirection * shootPower * -1f;
        rb.AddForce(push,ForceMode.Impulse);
    }
}
