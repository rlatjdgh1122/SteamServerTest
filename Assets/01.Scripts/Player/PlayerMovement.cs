using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float moveSpeed = 10f; // �ӵ� ������ ���� float�� ����

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }
}
