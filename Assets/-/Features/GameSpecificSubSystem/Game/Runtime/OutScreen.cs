using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class OutScreen : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_playerTransform.position.y < -5)
            {
                _onDeath.Invoke();
            }
        }
        
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private UnityEvent _onDeath;
    }
}
