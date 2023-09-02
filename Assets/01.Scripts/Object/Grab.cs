using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    //만약 걸린다면 적의 움직임을 멈추고 플레이어도 시전시간동안 일시적으로 멈춤
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
