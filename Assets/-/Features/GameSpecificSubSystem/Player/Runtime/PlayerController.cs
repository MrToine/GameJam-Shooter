using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerController : MonoBehaviour
    {
    

        #region Publics

        public void ResetState()
        {
            _state = PlayerState.IDLE;
            _velocity = Vector2.zero;
            _direction = Vector2.zero;
            _canJump = true;
            _isGrounded = true;
            if (_animator == null)
                _animator = GetComponent<Animator>();
            _animator.SetBool("walk", false);
            _animator.SetBool("jump", false);
            _animator.SetBool("TakeDamage", false);
            enabled = true;
            gameObject.SetActive(true);

            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_collider == null)
                _collider = GetComponent<PolygonCollider2D>();
            if (_rb != null)
            {
                _rb.simulated = true;
                _rb.linearVelocity = Vector2.zero;
                _rb.angularVelocity = 0f;
            }
            if (_spriteRenderer != null)
                _spriteRenderer.enabled = true;
            if (_collider != null)
                _collider.enabled = true;
            if (_muzzle != null)
                _muzzle.SetActive(true);
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _direction = Vector2.zero;
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

        private void Update()
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = IsGrounded();
            
            // Gérer les transitions d'animations basées sur l'état au sol
            if (!wasGrounded && _isGrounded)
            {
                // Vient de toucher le sol
                _animator.SetBool("jump", false);
                
                // Revenir à l'état IDLE ou WALK selon si le joueur se déplace
                if (_direction.x != 0)
                {
                    _state = PlayerState.WALK;
                }
                else
                {
                    _state = PlayerState.IDLE;
                }
            }
            
            // Mettre à jour les animations en fonction de l'état du joueur
            UpdateAnimations();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            switch (_state)
            {
                case PlayerState.IDLE:
                    IdleLoop();
                    break;
                case PlayerState.WALK:
                    WalkingLoop();
                    break;
                case PlayerState.JUMP:
                    _velocity.x = _direction.x * _speed;
                    CheckJump();
                    break;
                case PlayerState.HIT:
                    break;
                default:
                    IdleLoop();
                    break;
            }
            _rb.linearVelocityX = _velocity.x;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _canJump = true;
        }

        #endregion
    
        
        #region Main Methods

        private void UpdateAnimations()
        {
            // Mise à jour des animations selon l'état actuel
            switch (_state)
            {
                case PlayerState.IDLE:
                    _animator.SetBool("walk", false);
                    _animator.SetBool("jump", false);
                    break;
                case PlayerState.WALK:
                    _animator.SetBool("walk", true);
                    _animator.SetBool("jump", false);
                    break;
                case PlayerState.JUMP:
                    _animator.SetBool("walk", false);
                    _animator.SetBool("jump", true);
                    break;
                case PlayerState.HIT:
                    _animator.SetBool("TakeDamage", true);
                    break;
            }
        }

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
            _canJump = false;
        }

        private void CheckJump()
        {
            if (!_isGrounded && _rb.linearVelocityY < 0)
            {
                _rb.AddForce(-Vector2.up * _apexForce);
            }
        }
        
        private bool IsGrounded()
        {
            float rayLength = 1f;
            Vector2 origin = _jumpDetectorPos.position;
            Vector2 direction = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, LayerMask.GetMask("floor"));

            if (hit.collider != null)
            {
                return true;
            }
            return false;
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
        [SerializeField] private Transform _jumpDetectorPos;
        
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