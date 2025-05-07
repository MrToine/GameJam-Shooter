using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class Menus : MonoBehaviour
    {


        #region Publics

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartGame()
        {
            //
        }

        public void ColorEnterStart()
        {
            _startButtonSprite.color = new Color(1f, 1f, 1f, 0f);
        }

        public void ColorEnterQuit()
        {
            _quitButtonSprite.color = new Color(1f, 1f, 1f, 0f);
        }

        public void ColorDefault()
        {
            _startButtonSprite.color = new Color(1f, 1f, 1f, 0f);
            _quitButtonSprite.color = new Color(1f, 1f, 1f, 0f);
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        #endregion
    


        #region Main Methods

        //
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        [SerializeField] private SpriteRenderer _startButtonSprite;
        [SerializeField] private SpriteRenderer _quitButtonSprite;

        #endregion
    }
}
