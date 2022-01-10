// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class Player : MonoBehaviour
// {
//     public float moveSpeed;
//     [Range(1,100)] public int moveSpeedRampUp = 10;
//     [Range(1,100)] public int moveSpeedRampDown = 10;
//     [SerializeField] float jumpBuffer = 0.1f;
//     [SerializeField] float jumpSpeed;
//     [SerializeField] float jumpMaxTime = 1;

//     Vector2 dPadState;
        
//     float lastPressedJump = -1;
//     bool isGrounded = true;
//     bool jumpButtonIsHeld = false;
//     [HideInInspector] public bool jumpBufferActive = false;
//     [HideInInspector] public bool isJumping = false;
    
//     // At Start Variables
//     Rigidbody2D _playerBody;
//     Animator _animator;

//     // State Machine
//     PlayerBaseState currentState;
//     [HideInInspector] readonly public PlayerIdleState idleState = new PlayerIdleState();
//     [HideInInspector] readonly public PlayerRunState runState = new PlayerRunState();
//     [HideInInspector] readonly public PlayerCrouchState crouchState = new PlayerCrouchState();
//     [HideInInspector] readonly public PlayerJumpState jumpState = new PlayerJumpState();
//     [HideInInspector] readonly public PlayerSlideState slideState = new PlayerSlideState();
//     [HideInInspector] readonly public PlayerAttackState attackState = new PlayerAttackState();
//     bool transitioning = false;


//     void Awake()
//     {
//         _playerBody = GetComponent<Rigidbody2D>();
//         _animator = GetComponent<Animator>();
//     }

//     void Start()
//     {
//         currentState = idleState;
//         currentState.OnStateEnter(this);
//     }

//     public IEnumerator ChangeState(PlayerBaseState newState, float delay)
//     {
//         currentState.OnStateExit(this);
//         transitioning = true;
//         yield return new WaitForSeconds(delay);
//         transitioning = false;
//         currentState = newState;
//         currentState.OnStateEnter(this);
//     }


//     public void SetAnimator(string animation) { _animator.Play(animation); }

//     public Vector2 GetDPad() { return dPadState; }
//     public bool GetJumpButton() { return jumpButtonIsHeld; }
//     public Rigidbody2D GetPlayerBody() { return _playerBody; }

//     void Update()
//     {
//         currentState.StateUpdate(this);
//     }

//     void FixedUpdate()
//     {
//         if(!transitioning)
//         currentState.StateFixedUpdate(this);
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         currentState.StateHandleTrigger(this,other);
//     }

//     void OnCollisionEnter2D(Collision2D other)
//     {
//         currentState.StateHandleCollission(this,other);
//     }

//     void OnMove(InputValue dpad)
//     {
//         dPadState = dpad.Get<Vector2>();
//     }

//     void OnJump(InputValue button)
//     {
//         if(button.isPressed)
//         {
//             if(jumpBufferActive) { lastPressedJump = 0; }
//             else { StartCoroutine(JumpBuffer()); }
//         }
//         jumpButtonIsHeld = button.isPressed;
//     }

//     IEnumerator JumpBuffer()
//     {
//         jumpBufferActive = true;
//         lastPressedJump = 0;
//         do
//         {
//             lastPressedJump += Time.fixedDeltaTime;
//             yield return new WaitForFixedUpdate();
//         }
//         while (lastPressedJump <= jumpBuffer);
//         jumpBufferActive = false;
//     }

//     public void MovePlayer()
//     {
//         float delta = _playerBody.velocity.x;
//         delta += (dPadState.x*moveSpeed*(1/(float)moveSpeedRampUp))*Time.fixedDeltaTime;
//         _playerBody.velocity = new Vector2(Mathf.Clamp(delta,-moveSpeed*Time.fixedDeltaTime,moveSpeed*Time.fixedDeltaTime),_playerBody.velocity.y);
//     }   

//     public void SlowPlayer()
//     {
//         float delta = _playerBody.velocity.x;
//         if (delta > 0f)
//         {
//             delta -= (moveSpeed*(1/(float)moveSpeedRampDown))*Time.fixedDeltaTime;
//             delta = Mathf.Clamp(delta,Mathf.Epsilon,moveSpeed*Time.fixedDeltaTime);
//         }
//         if (delta < 0f)
//         {
//             delta += (moveSpeed*(1/(float)moveSpeedRampDown))*Time.fixedDeltaTime;
//             delta = Mathf.Clamp(delta,-moveSpeed*Time.fixedDeltaTime,Mathf.Epsilon);
//         }
//         _playerBody.velocity = new Vector2(delta,_playerBody.velocity.y);
//     }

//     public IEnumerator PlayerJump()
//     {
//         float hasBeenJumping = 0;
//         lastPressedJump += jumpBuffer;
//         while (isJumping && jumpButtonIsHeld && hasBeenJumping <= jumpMaxTime)
//         {
//             _playerBody.velocity = new Vector2(_playerBody.velocity.x,jumpSpeed*Time.deltaTime);
//             hasBeenJumping =+ Time.fixedDeltaTime;
//             yield return new WaitForFixedUpdate();
//         }
//         isJumping = false;
//     }

// }
