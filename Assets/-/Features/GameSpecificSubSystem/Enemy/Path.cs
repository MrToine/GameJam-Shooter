using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Runtime
{
    public class Path : MonoBehaviour
    {


        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            //
        }

        private void Start()
        {
            //
        }

        // Update is called once per frame
        void Update()
        {
            //
        }

        #endregion
    


        #region Main Methods

        //
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected
        
        [Header("Liste des points du chemin de l'enemie")]
        [SerializeField] private List<GameObject> _pointsPath = new List<GameObject>();

        #endregion
    }
}
