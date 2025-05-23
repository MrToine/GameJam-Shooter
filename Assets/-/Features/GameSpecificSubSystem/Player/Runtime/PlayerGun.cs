using Game.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerGun : MonoBehaviour
    {


        #region Publics

        public void OnAttack(InputAction.CallbackContext context)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("idle"))
            {
                _animator.SetTrigger("fire");
            }
            else if (stateInfo.IsName("walk"))
            {
                _animator.SetTrigger("walk+fire");
            }
            if (context.performed)
            {
                _impactSound.Play();
                var bullet = _poolSystem.GetFirstAvailableProjectile();
                bullet.transform.position = _muzzle.transform.position;
                bullet.SetActive(true);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                Transform particleTransform = _particleSystem.transform.GetComponent<Transform>();
                if (transform.localScale.x > 0)
                {   
                    particleTransform.localScale = new Vector3(1, 1, 1);
                    bulletRb.AddForce(Vector2.right * _bulletSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    particleTransform.localScale = new Vector3(-1, -1, -1);                    
                    bulletRb.AddForce(Vector2.left * _bulletSpeed, ForceMode2D.Impulse);
                }

                _particleSystem.Play();
            }
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _playerSprite = GetComponent<SpriteRenderer>();
            _animator =  GetComponent<Animator>();
        }

        #endregion
    


        #region Main Methods

        //
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private SpriteRenderer _playerSprite;
        private Animator _animator;
        
        [Header("Game Object <Muzzle>")]
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private PoolSystem _poolSystem;

        [Header("Vitesse de tire")]
        [SerializeField] private float _bulletSpeed = 5;
        
        [SerializeField] private ParticleSystem _particleSystem;
        
        [Header("Son")]
        [SerializeField] private AudioSource _impactSound;

        #endregion
    }
}
