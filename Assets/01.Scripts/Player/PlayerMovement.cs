using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float moveSpeed = 10f; // 속도 조절을 위해 float로 변경

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }
}
