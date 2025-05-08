using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerController : MonoBehaviour
    {
    

        #region Publics

        public float GetPlayerDirection()
        {
            return _direction.x;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _state = PlayerState.IDLE;
            }
            else
            {
                _state = PlayerState.WALK;
                _direction = context.ReadValue<Vector2>();
                if (_direction.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    //_muzzle.transform.localPosition = new Vector3(_muzzlPos.x, _muzzlPos.y, 0);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    //_muzzle.transform.localPosition = new Vector3(-_muzzlPos.x, _muzzlPos.y, 0);
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && _isGrounded)
            {
                Jump();
            }
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _velocity = _rb.linearVelocity;
            _muzzlPos = _muzzle.transform.localPosition;
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            switch (_state)
            {
                case PlayerState.IDLE:
                    _animator.SetBool("walk", false);
                    IdleLoop();
                    break;
                case PlayerState.WALK:
                    _animator.SetBool("walk", true);
                    _animator.SetBool("jump", false);
                    WalkingLoop();
                    break;
                case PlayerState.JUMP:
                    _animator.SetBool("walk", false);
                    _animator.SetBool("jump", true);
                    CheckJump();
                    break;
                case PlayerState.HIT:
                    _animator.SetBool("TakeDamage", true);
                    break;
                default:
                    IdleLoop();
                    break;
            }
            _rb.linearVelocityX = _velocity.x;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isGrounded = true;
            _canJump = true;
        }

        #endregion
    
        
        #region Main Methods

        private void IdleLoop()
        {
            _velocity.x = 0;
        }
        
        private void WalkingLoop()
        {
            _velocity.x += _direction.x * _acceleration * Time.deltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, _speed);
        }

        private void Jump()
        {
            //_rb.gravityScale = 2.5f; ==> POUR DIMINUER LA LONGUEUR DU SAUT VIA LA GRAVITE
            _state = PlayerState.JUMP;
            float jumpVelocity = Mathf.Sqrt(2 * 9.81f * _jumpHeight);
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpVelocity);
            _isGrounded = false;
            _canJump = false;
        }

        private void CheckJump()
        {
            if (!_isGrounded && _rb.linearVelocityY < 0)
            {
                _rb.AddForce(-Vector2.up * _apexForce);
            }
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _velocity;
        private PlayerState _state;
        private Vector2 _direction;
        private bool _canJump = true;
        private Vector2 _muzzlPos;
        private Animator _animator;
        private PolygonCollider2D _collider;

        [Header("Références")] 
        [SerializeField] private GameObject _muzzle;
        
        [SerializeField] private bool _isGrounded;
        
        [Header("Vitesse, Accélération et Décélération")]
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _acceleration = 50;
        [SerializeField] private float _deceleration = 100;
        
        [Header("Hauteur du saut et vitesse de déscente")]
        [SerializeField] private float _jumpForce = 10;
        [SerializeField] private float _apexForce = 10;
        [SerializeField] private float _jumpHeight = 5;

        private enum PlayerState
        {
            IDLE,
            WALK,
            JUMP,
            HIT
        }

        #endregion
    }
}
