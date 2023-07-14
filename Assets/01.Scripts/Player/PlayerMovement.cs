using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 10;

    public void OnMove(Vector2 movePos)
    {
        transform.position = Vector3.Lerp(transform.position, movePos, 0.02f);
    }
}
