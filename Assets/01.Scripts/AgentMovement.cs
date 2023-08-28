using System.Collections.Generic;
using UnityEngine;
using Core;
using System;
using Mirror;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using UnityEngine.TextCore.Text;

public class AgentMovement : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float moveSpeed = 3;
    public float currentSpeed = 0;

    public bool isRun = false;


    public Dictionary<WayType, GameObject> wayObjects = new();
    public WayType currnetWayType;

    private Vector2 inputVec { get; set; }
    private Vector2 checkVec { get; set; }


    private Rigidbody2D _rb;
    private Character4D _character;
    private void Awake()
    {
        _rb = (Rigidbody2D)GetComponent("Rigidbody2D");
        _character = (Character4D)GetComponent("Character4D");
    }
    void Start()
    {
        foreach (WayType item in Enum.GetValues(typeof(WayType)))
        {
            GameObject obj = transform.Find($"Sprite/{item}").gameObject;
            wayObjects.Add(item, obj);
        }
        _character.AnimationManager.SetState(CharacterState.Idle);

        currentSpeed = moveSpeed;

        _inputReader.MovementEvent += HandleMovement;
        _inputReader.RunEvent += HandleRun;
    }

    private void HandleRun(bool value)
    {
        Debug.Log("달리다");
        isRun = value;

        if (value)
        {
            moveSpeed *= 1.5f;
        }
        moveSpeed = currentSpeed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = (inputVec * moveSpeed);
    }
    private void HandleMovement(Vector2 move)
    {
        inputVec = move;

        if (inputVec.x > 0) currnetWayType = WayType.Right;
        else if (inputVec.x < 0) currnetWayType = WayType.Left;
        else if (inputVec.y > 0) currnetWayType = WayType.Back;
        else if (inputVec.y < 0) currnetWayType = WayType.Front;
        else currnetWayType = WayType.Front;

        SetAnimation(inputVec);
    }
    private void SetAnimation(Vector2 dir)
    {
        if (checkVec == dir) return;
        checkVec = dir;

        wayObjects.Select(currnetWayType,
            p => p.SetActive(true),
            p => p.SetActive(false));

        Debug.Log("애니메이션");

        if (isRun)
            _character.AnimationManager.SetState(CharacterState.Run);
        else if (!isRun)
            _character.AnimationManager.SetState(CharacterState.Idle);
        Debug.Log("애니메이션끝");
    }
    private void OnDestroy()
    {
        _inputReader.MovementEvent -= HandleMovement;
        _inputReader.RunEvent -= HandleRun;
    }
}
