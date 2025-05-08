using System;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class Chrono : MonoBehaviour
    {


        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _textMeshChrono = _chrono.GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            TimeSpan timeSpan = TimeSpan.FromSeconds(_elapsedTime);
            string timeFormated = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            
            _textMeshChrono.text = timeFormated + " sec";
        }

        #endregion
        

        #region Main Methods

        //
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private float _elapsedTime = 0f;
        private TMP_Text _textMeshChrono = null;
        
        [SerializeField] private GameObject _chrono;

        #endregion
    }
}
