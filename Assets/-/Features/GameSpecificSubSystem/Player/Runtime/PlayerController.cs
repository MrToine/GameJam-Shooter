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
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
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
        }

        #endregion
    
        
        #region Main Methods

        private void IdleLoop()
        {
            Debug.Log("Idle Loop");
            _rb.linearVelocityX = 0;
        }
        
        private void WalkingLoop()
        {
            //_rb.MovePosition(_direction * _speed * Time.deltaTime);
            _rb.linearVelocityX += _direction.x * _speed * Time.deltaTime;
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private Rigidbody2D _rb;
        private PlayerState _state;
        private Vector2 _direction;
        private float _speed = 50;

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
