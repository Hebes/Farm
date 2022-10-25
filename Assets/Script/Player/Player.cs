using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移动相关")]
    private Rigidbody2D rb;
    public float speed = 10f;
    private float inputX;
    private float inputY;
    private Vector2 movementInput;

    [Header("动画相关")]
    private Animator[] animators;
    private bool isMoving;

    /// <summary>
    /// 玩家不能操作
    /// </summary>
    private bool InputDisable;

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition += OnMoveToPosition;
        EventHandler.MouseClickedEvent += OnMouseClickedEvent;
            
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
        EventHandler.MouseClickedEvent -= OnMouseClickedEvent;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }
    void Update()
    {
        if (!InputDisable)
            PlayerInput();
        else
            isMoving = false;
        SwitchAnimation();
    }
    private void FixedUpdate()
    {
        if (!InputDisable)
            Movement();
    }

    private void OnBeforeSceneUnloadEvent() => InputDisable = true;
    private void OnAfterSceneLoadedEvent() => InputDisable = false;
    private void OnMoveToPosition(Vector3 targetPosition) => transform.position = targetPosition;
    private void OnMouseClickedEvent(Vector3 pos, ItemDetails itemDetails)
    {
        EventHandler.CallExecuteActionAfterAnimation(pos, itemDetails);
    }

    /// <summary>玩家输入</summary>
    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 && inputY != 0)//防止左上等移动的时候超过1
        {
            inputX = inputX * 0.6f;
            inputY = inputY * 0.6f;
        }

        //走路速度下降
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX * 0.5f;
            inputY = inputY * 0.5f;
        }

        movementInput = new Vector2(inputX, inputY);

        isMoving = movementInput != Vector2.zero;//判断是否在移动
    }
    /// <summary>角色移动</summary>
    private void Movement() => rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * movementInput));
    /// <summary>播放所有的动画</summary>
    private void SwitchAnimation()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("IsMoving", isMoving);//注意IsMoving要和动画那边的一样
            if (isMoving)
            {
                anim.SetFloat("InputX", inputX);
                anim.SetFloat("InputY", inputY);
            }
        }
    }
}
