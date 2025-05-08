using System;
using UnityEngine;

namespace Enemy.Runtime
{
    public class Enemy : MonoBehaviour
    {
        #region Publics

        public void GoToPlayer(GameObject player)
        {
            _lastPosition = transform.position;
            _newPosition = player.transform.position;
            _direction = _newPosition - _lastPosition;
            _speed /= 2;
            _state = EnemyState.CHASING;
        }

        public void EscapeToPlayer()
        {
            _speed *= 2;
            _direction = _lastPosition - _newPosition;
            _state = EnemyState.RETURNING;
        }
    
        #endregion
        

        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _transform = transform;
            _rb = GetComponent<Rigidbody2D>();
            _velocity = _rb.linearVelocity; 
        }
        
        void Start()
        {
            _pathsGroup.SetActive(true);
            _floorCollider.SetActive(false);
            _inPath = true;
            _state = EnemyState.ONLIFE;
            _direction.x = -1;
        }

        // Update is called once per frame
        void Update()
        {
            switch (_state)
            {
                case EnemyState.ONLIFE:
                    CheckState();
                    break;
                case EnemyState.INPATH:
                    Start();
                    break;
                case EnemyState.CHASING:
                    _pathsGroup.SetActive(false);
                    _floorCollider.SetActive(true);
                    _inPath = false;
                    break;
                case EnemyState.RETURNING:
                    CheckPosInPath();
                    break;
                case EnemyState.DEAD:
                    Kill();
                    break;
            }
        }
        
        void FixedUpdate()
        {
            _rb.linearVelocity = _direction * _speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                _life -= 1;
            }
            
            else if (other.gameObject.layer == LayerMask.NameToLayer("Path") && _inPath)
            {
                Debug.Log("Collision Enter with path");
                _direction = new Vector2(-_direction.x, _direction.y);
                _sprite.flipX = -_direction.x < 0;
            }
        }

        #endregion
        

        #region Main Methods

        private void Kill()
        {
            gameObject.SetActive(false);
            _pathsGroup.SetActive(false);
        }

        private void CheckState()
        {
            if (_life <= 0)
            {
                _state = EnemyState.DEAD;
            }
        }

        private void CheckPosInPath()
        {
            float distance = Vector2.Distance(transform.position, _lastPosition);
            if (distance < 0.3f)
            {
                _rb.linearVelocity = Vector2.zero;
                _direction = Vector2.zero;
                _state = EnemyState.INPATH;
            }
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected
        
        private Vector2 _direction;
        private Transform _transform;
        private Rigidbody2D _rb;
        private Vector2 _velocity;
        private EnemyState _state;
        private Vector2 _lastPosition;
        private Vector2 _newPosition;
        private SpriteRenderer _sprite;
        private bool _inPath = true;
        
        [SerializeField] private GameObject _pathsGroup;
        [SerializeField] private GameObject _floorCollider;
        
        [Header("Stats de l'enemie")] 
        [SerializeField] private int _life = 1;
        
        [Header("Vitesse de l'enemie")]
        [SerializeField] private float _speed;

        private enum EnemyState
        {
            ONLIFE,
            INPATH,
            CHASING,
            RETURNING,
            DEAD
        }

        #endregion
    }
}
