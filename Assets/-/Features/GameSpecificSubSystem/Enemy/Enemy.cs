using System;
using UnityEngine;

namespace Enemy.Runtime
{
    public class Enemy : MonoBehaviour
    {


        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _state = EnemyState.ONLIFE;
        }

        // Update is called once per frame
        void Update()
        {
            switch (_state)
            {
                case EnemyState.ONLIFE:
                    CheckState();
                    break;
                case EnemyState.DEAD:
                    Kill();
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            LayerMask mask = LayerMask.GetMask("Bullet");
            if (other.gameObject.layer == mask)
            {
                Debug.Log("touched by " + other.gameObject.name);
                _life -= 1;
            }
        }

        #endregion
    


        #region Main Methods

        private void Kill()
        {
            gameObject.SetActive(false);
        }

        private void CheckState()
        {
            if (_life <= 0)
            {
                _state = EnemyState.DEAD;
            }
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private EnemyState _state;
        [Header("Stats de l'enemie")] 
        [SerializeField] private int _life = 1;

        private enum EnemyState
        {
            ONLIFE,
            DEAD
        }

        #endregion
    }
}
