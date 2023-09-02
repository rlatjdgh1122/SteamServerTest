using System.Collections.Generic;
using UnityEngine;
using Core;
using System;
using Mirror;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public enum MOVE_TYPE
{
    Idle,
    Walk,
    Run
}
public class AgentMovement : NetworkBehaviour
{
    private AnimationManager _anim;

    [SerializeField] private InputReader _inputReader;
    public float _moveSpeed = 3;
    public float _runSpeed = 5;
    private float _currentSpeed = 0;
    private float MaxSpeed = 5;
    public float CurrentSpeed
    {
        set
        {
            _currentSpeed = Mathf.Clamp(value, 0, MaxSpeed);
        }
        get { return _currentSpeed; }
    }

    private bool isRun = false;
    private bool isIdle = false;


    public Dictionary<WayType, GameObject> wayObjects = new();
    public WayType currnetWayType;

    private Vector2 inputVec { get; set; }
    private Vector2 checkVec { get; set; }


    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = (Rigidbody2D)GetComponent("Rigidbody2D");
        _anim = (AnimationManager)transform.Find("Charater").GetComponent("AnimationManager");

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
        _anim.SetState(CharacterState.Idle);

        SetSpeed(MOVE_TYPE.Idle);
    }


    private void FixedUpdate()
    {
        _rb.velocity = (inputVec * CurrentSpeed);
    }
    private void HandleRun(bool value)
    {
        Debug.Log(value);
        if (isIdle) return;

        if (value)
        {
            isRun = true;

            _anim.SetState(CharacterState.Run);
            SetSpeed(MOVE_TYPE.Run);
        }
        else isRun = false;
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

        if (!isRun)
        {
            if (inputVec == Vector2.zero)
            {
                isIdle = true;

                _anim.SetState(CharacterState.Idle);
                SetSpeed(MOVE_TYPE.Idle);
            }
            else
            {
                _anim.SetState(CharacterState.Walk);
                SetSpeed(MOVE_TYPE.Walk);
            }
        }
        else
            isIdle = false;
    }
    public void SetSpeed(MOVE_TYPE type)
    {
        if (type == MOVE_TYPE.Idle)
            CurrentSpeed = 0;
        else if (type == MOVE_TYPE.Walk)
            CurrentSpeed = _moveSpeed;
        else if (type == MOVE_TYPE.Run)
            CurrentSpeed = _runSpeed;
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
