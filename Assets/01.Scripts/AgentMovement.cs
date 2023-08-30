using System.Collections.Generic;
using UnityEngine;
using Core;
using System;
using Mirror;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class AgentMovement : NetworkBehaviour
{
    public Character4D _character;
    [SerializeField] private InputReader _inputReader;
    public float _moveSpeed = 3;
    private float _currentSpeed = 0;
    public float CurrentSpeed
    {
        set
        {
            if (isRun)
                _currentSpeed = Mathf.Clamp(value * 1.5f, 0, 5);
            else
                _currentSpeed = Mathf.Clamp(value, 0, 5);
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

        CurrentSpeed = _currentSpeed;
    }

    private void HandleRun(bool value)
    {
        Debug.Log(value);
        if (isIdle) return;
        if (value)
        {
            isRun = true;

            Debug.Log("´Þ¸®´Ù");
            _character.AnimationManager.SetState(CharacterState.Run);
            CurrentSpeed = _moveSpeed * 1.5f;
        }
        else isRun = false;
    }

    private void FixedUpdate()
    {
        _rb.velocity = (inputVec * CurrentSpeed);
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
                _character.AnimationManager.SetState(CharacterState.Idle);
            }
            else
                _character.AnimationManager.SetState(CharacterState.Walk);
        }
        else
            isIdle = false;
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
