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
        }

        private void Update()
        {
            if (_lifePoints <= 0)
            {
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
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Entered on collision with :" + other.gameObject.name);
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
            {
                _lifePoints--;
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
        
        [SerializeField] private int _lifePoints;
        [SerializeField] private UnityEvent _changeState;

        #endregion
    }
}
