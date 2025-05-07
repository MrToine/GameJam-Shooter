using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PoolSystem : MonoBehaviour
    {
        #region Publics

        //

        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject projectile = Instantiate(_projectilePrefab);
                projectile.SetActive(false);
                _listOfProjectile.Add(projectile);
            }
        }

        #endregion
        

        #region Main Methods

        public GameObject GetFirstAvailableProjectile()
        {
            foreach (var instance in _listOfProjectile)
            {
                if (!instance.activeSelf)
                {
                    return instance;
                }
            }

            var newInstance = Instantiate(_projectilePrefab, transform);
            newInstance.SetActive(false);
            _listOfProjectile.Add(newInstance);
            return newInstance;
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private int _poolSize = 10;

        private List<GameObject> _listOfProjectile = new List<GameObject>();

        #endregion
    }

}