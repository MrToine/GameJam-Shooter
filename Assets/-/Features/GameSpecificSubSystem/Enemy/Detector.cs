using System;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Runtime
{
    public class Detector : MonoBehaviour
    {


        #region Publics

        //
    
        #endregion


        #region Unity API

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _goToPlayer.Invoke(_player);
                _shootPlayer.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
               _goToEnemyPath.Invoke();
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
        
        [SerializeField] private GameObject _player;
        [SerializeField] private UnityEvent<GameObject> _goToPlayer;
        [SerializeField] private UnityEvent _goToEnemyPath;
        [SerializeField] private UnityEvent _shootPlayer;

        #endregion
    }
}