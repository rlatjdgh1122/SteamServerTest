using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    //���� �ɸ��ٸ� ���� �������� ���߰� �÷��̾ �����ð����� �Ͻ������� ����
    public Vector2 direction { get; private set; }
    public void SetDirection(Vector2 value)
    {
        direction = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = collision.transform.position;

        if (collision.TryGetComponent(out AgentMovement agentMovement))
        {

        }
    }
}
