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
    public Character4D _character;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float moveSpeed = 3;
    public float currentSpeed = 0;

    public bool isRun = false;


    public Dictionary<WayType, GameObject> wayObjects = new();
    public WayType currnetWayType;

    private Vector2 inputVec { get; set; }
    private Vector2 checkVec { get; set; }


    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = (Rigidbody2D)GetComponent("Rigidbody2D");

        _inputReader.MovementEvent += HandleMovement;
        _inputReader.RunEvent += HandleRun;
    }
    void Start()
    {
        foreach (WayType item in Enum.GetValues(typeof(WayType)))
        {
            GameObject obj = transform.Find($"Charater/{item}").gameObject;
            wayObjects.Add(item, obj);
        }
        _character.AnimationManager.SetState(CharacterState.Idle);

        currentSpeed = moveSpeed;
    }

    private void HandleRun(bool value)
    {
        Debug.Log("´Þ¸®´Ù");
        _character.AnimationManager.SetState(CharacterState.Run);
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

        if (inputVec == Vector2.zero)
            _character.AnimationManager.SetState(CharacterState.Idle);
        else
            _character.AnimationManager.SetState(CharacterState.Walk);
    }
    private void SetAnimation(Vector2 dir)
    {
        if (checkVec == dir) return;
        checkVec = dir;

        wayObjects.Select(currnetWayType,
            p => p.SetActive(true),
            p => p.SetActive(false));
    }
    private void OnDestroy()
    {
        _inputReader.MovementEvent -= HandleMovement;
        _inputReader.RunEvent -= HandleRun;
    }
}
