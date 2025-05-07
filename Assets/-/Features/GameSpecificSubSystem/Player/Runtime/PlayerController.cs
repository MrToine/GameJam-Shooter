using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerController : MonoBehaviour
    {
    

        #region Publics

        public Vector2 m_direction;
        
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
                _spriteRenderer.flipX = _direction.x < 0;
                if (_spriteRenderer.flipX)
                {
                    _muzzle.transform.localPosition = new Vector3(-0.49f, 0, 0);
                }
                else
                {
                    _muzzle.transform.localPosition = new Vector3(0.49f, 0, 0);
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
        }

        // Update is called once per frame
        private void Update()
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
            _state = PlayerState.JUMP;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
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
        //[SerializeField] private float _jumpSpeed = 300;

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
