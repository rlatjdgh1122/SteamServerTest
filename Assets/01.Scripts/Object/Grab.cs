using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    //���� �ɸ��ٸ� ���� �������� ���߰� �÷��̾ �����ð����� �Ͻ������� ����
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
        direction = value;
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
        transform.parent = trm.transform;

        if (collision.attachedRigidbody.TryGetComponent(out Igrabable component))
        {
            component.SetGrabt(direction, power, gameObject);
        }
    }
}
