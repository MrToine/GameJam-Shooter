using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerController : MonoBehaviour
    {


        #region Publics

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
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && _isGrounded)
            {
                Debug.Log("Jumping");
                Jump();
            }
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            Debug.Log(collision.gameObject.name);
            _isGrounded = true;
        }

        #endregion
    
        
        #region Main Methods

        private void IdleLoop()
        {
            /*Vector2 decelerationVector = (_velocity.normalized * _deceleration) * Time.deltaTime; 
            _velocity -= decelerationVector; 
            _rb.linearVelocity = _velocity;*/
            _velocity.x = 0;
        }
        
        private void WalkingLoop()
        {
            _velocity.x += _direction.x * _acceleration * Time.deltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, _speed);
        }

        private void Jump()
        {
            Debug.Log("Spriiiiing");
            _state = PlayerState.JUMP;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private Rigidbody2D _rb;
        private Vector2 _velocity;
        private PlayerState _state;
        private Vector2 _direction;
        [SerializeField] private bool _isGrounded;
        
        [Header("Vitesse, Accélération et Décélération")]
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _acceleration = 50;
        [SerializeField] private float _deceleration = 100;
        
        [Header("Hauteur, longueur du saut")]
        [SerializeField] private float _jumpForce = 300;
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
