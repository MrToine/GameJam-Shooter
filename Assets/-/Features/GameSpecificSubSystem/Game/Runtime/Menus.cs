using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class Menus : MonoBehaviour
    {


        #region Publics

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void StartGame()
        {
            //
        }
        

        public void ScaleButton(GameObject button)
        {
            Debug.Log("ScaleButton");
            button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        
        public void UnscaleButton(GameObject button)
        {
            Debug.Log("UnscaleButton");
            button.transform.localScale = new Vector3(1, 1, 1);
        }

        public void ActiveMenu()
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }

        public void DeactiveMenu()
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }

        public void Death()
        {
            Time.timeScale = 0;
            if (_deathScreen != null)
            {
                TMP_Text finalChrono = _deathScreen.GetComponentInChildren<TMP_Text>();
                finalChrono.text = _textMeshChrono.text;
                _deathScreen.SetActive(true);
            }
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            /*GameObject[] allObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Debug.Log(obj.name);
                if (obj != gameObject || obj.transform.IsChildOf(gameObject.transform) || obj.name != "Main Camera" || obj.name != "canvas")
                {
                    obj.SetActive(false);
                }
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                _isPaused = !_isPaused;
                if (_isPaused)
                {
                    ActiveMenu();
                }
                else
                {
                    DeactiveMenu();
                }
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
        
        private bool _isPaused = false;
        
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private TMP_Text _textMeshChrono;

        #endregion
    }
}
