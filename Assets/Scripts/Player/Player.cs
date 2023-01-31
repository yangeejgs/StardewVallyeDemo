using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class Player : SingletonMonoBehavior<Player>
{
    // Movement Parameters
    public float xInput;
    public float yInput;
    public bool isWalking;
    public bool isRunning;
    public bool isIdle;
    public bool isCarrying;
    public ToolEffect toolEffect;
    public bool isUsingToolRight;
    public bool isUsingToolLeft;
    public bool isUsingToolUp;
    public bool isUsingToolDown;
    public bool isLiftingToolRight;
    public bool isLiftingToolLeft;
    public bool isLiftingToolUp;
    public bool isLiftingToolDown;
    public bool isPickingRight;
    public bool isPickingLeft;
    public bool isPickingUp;
    public bool isPickingDown;
    public bool isSwingingToolRight;
    public bool isSwingingToolLeft;
    public bool isSwingingToolUp;
    public bool isSwingingToolDown;
    public bool idleRight;
    public bool idleLeft;
    public bool idleUp;
    public bool idleDown;

    private Camera mainCamera;
    private Rigidbody2D rbody;
    private Direction playerDirection;

    private float movementSpeed;
    private bool playerInputIsDisabled = false;
    public bool PlayerInputIsDisabled { get => playerInputIsDisabled; set => playerInputIsDisabled = value; }

    protected override void Awake()
    {
        base.Awake();
        rbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        #region Player Input
        if (!PlayerInputIsDisabled)
        {
            // 重置状态
            ResetAnimationTriggers();
            // 跑步输入控制
            PlayerMovementInput();
            // 按住shift控制走路
            PlayerWalkInput();
            // 将事件发送给任何收听者以进行玩家移动输入
            EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying,
            toolEffect,
            isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
            isLiftingToolRight, isLiftingToolRight, isLiftingToolUp, isLiftingToolDown,
            isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
            isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
            idleRight, idleLeft, idleUp, idleDown);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 v = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);
        rbody.MovePosition(rbody.position + v);
    }

    private void PlayerWalkInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Settings.walkingSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;
        }
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (xInput != 0 && yInput != 0)
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;
            if (xInput > 0)
            {
                playerDirection = Direction.right;
            }
            else if (xInput < 0)
            {
                playerDirection = Direction.left;
            }
            if (yInput > 0)
            {
                playerDirection = Direction.up;
            }
            if (yInput < 0)
            {
                playerDirection = Direction.down;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }

    }

    private void ResetAnimationTriggers()
    {
        isPickingRight = false;
        isPickingLeft = false;
        isPickingUp = false;
        isPickingDown = false;

        isUsingToolRight = false;
        isUsingToolLeft = false;
        isUsingToolUp = false;
        isUsingToolDown = false;

        isLiftingToolRight = false;
        isLiftingToolLeft = false;
        isLiftingToolUp = false;
        isLiftingToolDown = false;

        isSwingingToolRight = false;
        isSwingingToolLeft = false;
        isSwingingToolUp = false;
        isSwingingToolDown = false;

        toolEffect = ToolEffect.none;
    }

    public Vector3 GetPlayerViewportPosition()
    {
        return mainCamera.WorldToViewportPoint(transform.position);
    }

    /// <summary>
    /// 开启玩家输入状态
    /// </summary>
    public void EnablePlayerInput()
    {
        PlayerInputIsDisabled = false;
    }

    /// <summary>
    /// 关闭玩家输入状态
    /// </summary>
    public void DisablePlayerInput()
    {
        PlayerInputIsDisabled = true;
    }

    /// <summary>
    /// 禁用玩家输入并且重置移动状态
    /// </summary>
    public void DisablePlayerInputAndResetMovement()
    {
        // 禁用玩家输入
        DisablePlayerInput();

        // 重置移动状态
        ResetMovement();

        // 将事件发送给任何收听者以进行玩家移动输入
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying,
        toolEffect,
        isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
        isLiftingToolRight, isLiftingToolRight, isLiftingToolUp, isLiftingToolDown,
        isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
        isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
        idleRight, idleLeft, idleUp, idleDown);
    }

    /// <summary>
    /// 重置移动状态
    /// </summary>
    private void ResetMovement()
    {
        xInput = 0;
        yInput = 0;
        isWalking = false;
        isRunning = false;
        isIdle = true;
    }
}
