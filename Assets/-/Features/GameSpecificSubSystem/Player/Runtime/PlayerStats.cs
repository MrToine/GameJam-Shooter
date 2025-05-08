using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Runtime
{
    public class PlayerStats : MonoBehaviour
    {
        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentScaleXLifeBar = _lifeUI.transform.localScale.x;
        }

        private void Update()
        {
            if (_lifePoints <= 0)
            {
                _onDeath.Invoke();
                Destroy(gameObject);
            }
            if (_isInvincible)
            {
                var timer = 1.0f;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    _isInvincible = false;
                    timer = 1.0f;
                }
            }

            _lifeUI.transform.localScale = new Vector3(_currentScaleXLifeBar, _lifeUI.transform.localScale.y, _currentScaleXLifeBar);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
            {
                _lifePoints--; ;
                _currentScaleXLifeBar -= 0.2f;
                _isInvincible = true;
            }
        }

        #endregion
        

        #region Main Methods

        //
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private bool _isInvincible = false;
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _lifeSprite;
        private float _currentScaleXLifeBar;

        [SerializeField] private UnityEvent _onDeath;
        [SerializeField] private int _lifePoints;
        [SerializeField] private UnityEvent _changeState;
        [SerializeField] private GameObject _lifeUI;

        #endregion
    }
}
