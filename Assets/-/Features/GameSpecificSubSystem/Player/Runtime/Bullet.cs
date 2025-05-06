using System;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Player.Runtime
{
    public class Bullet : MonoBehaviour
    {


        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _timer = 0;
            _rb = GetComponent<Rigidbody2D>();
            _gameObject = gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _gameObject.SetActive(false);
                _rb.linearVelocity = Vector2.zero;
                _rb.angularVelocity = 0;
                _timer = _timeLife;
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            LayerMask mask = LayerMask.NameToLayer("Enemy");
            if (other.gameObject.layer == mask)
            {
                gameObject.SetActive(false);
                _timer = _timeLife;
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

        private float _timer;
        private Rigidbody2D _rb;
        private GameObject _gameObject;

        [Header("Temps de vie d'une balle")] 
        [SerializeField] private float _timeLife;

        #endregion
    }
}
