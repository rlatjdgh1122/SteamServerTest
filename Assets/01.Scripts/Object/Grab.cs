using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    //만약 걸린다면 적의 움직임을 멈추고 플레이어도 시전시간동안 일시적으로 멈춤
    public Vector2 direction;
    private Rigidbody2D rb;
    private Collider2D coll;

    public float power = .2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 value)
    {
        direction = value.normalized;
    }
    public void SetThisCollider(Collider2D value)
    {
        coll = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == coll) return;
        rb.velocity = Vector3.zero;

        Transform trm = collision?.transform.root.transform.Find("Pivot").transform;

        transform.position = trm.position;

        if (collision.attachedRigidbody.TryGetComponent(out Igrabable component))
        {
            component.SetGrabt(direction, power);
        }
    }
}
