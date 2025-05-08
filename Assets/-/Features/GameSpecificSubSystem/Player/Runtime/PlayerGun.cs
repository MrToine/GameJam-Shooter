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
            if (context.performed)
            {
                var bullet = _poolSystem.GetFirstAvailableProjectile();
                bullet.transform.position = _muzzle.transform.position;
                bullet.SetActive(true);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                Transform particleTransform = _particleSystem.transform.GetComponent<Transform>();
                if (_playerSprite.flipX)
                {   
                    particleTransform.localScale = new Vector3(-1, -1, -1);
                    particleTransform.localPosition = new Vector3(-0.230f, 0, 0);
                    bulletRb.AddForce(Vector2.left * _bulletSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    particleTransform.localScale = new Vector3(1, 1, 1);                    
                    particleTransform.localPosition = new Vector3(0.230f, 0, 0);
                    bulletRb.AddForce(Vector2.right * _bulletSpeed, ForceMode2D.Impulse);
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
        
        [Header("Game Object <Muzzle>")]
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private PoolSystem _poolSystem;

        [Header("Vitesse de tire")]
        [SerializeField] private float _bulletSpeed = 5;
        
        [SerializeField] private ParticleSystem _particleSystem;

        #endregion
    }
}
