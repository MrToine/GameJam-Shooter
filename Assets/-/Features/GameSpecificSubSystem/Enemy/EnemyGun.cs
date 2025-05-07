using Game.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class EnemyGun : MonoBehaviour
    {
        #region Publics

        public void OnAttack()
        {
            Debug.Log("TIR");
            _canShoot = true;
        }

        public void DontAttack()
        {
            _canShoot = false;
        }
    
        #endregion


        #region Unity API

        private void Update()
        {
            if (_canShoot && Time.time - _lastShotTime >= _fireRate)
            {
                Shoot();
                _lastShotTime = Time.time;
            }
            else
            {
                Debug.Log("PAS DE PIOUUU");
            }
        }

        #endregion
    


        #region Main Methods

        private void Shoot()
        {
            var bullet = _poolSystem.GetFirstAvailableProjectile();
            bullet.transform.position = _muzzle.transform.position;
            bullet.SetActive(true);
                
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = (_player.transform.position - _muzzle.transform.position).normalized;
            bulletRb.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private bool _canShoot = false;
        private float _lastShotTime;
        
        [Header("Game Object <Muzzle> & Pool System")]
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private PoolSystem _poolSystem;
        [SerializeField] private GameObject _player;

        [Header("Vitesse des tirs & temps entre chaque tirs")]
        [SerializeField] private float _bulletSpeed = 5;
        [SerializeField] private float _fireRate = 0.5f;
        
        #endregion
    }
}
