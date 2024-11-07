using Game.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector3 _playerUIOffset;
        [SerializeField] private Image _fillImage;
        [SerializeField] private GameObject _objectToFollow;
        private IDamageable _damageable;
    
        private float _maxValue = 0;
        private float _currentValue = 0;

        private void Start()
        {
            if (_objectToFollow != null)
            {
                Setup(_objectToFollow.GetComponent<IDamageable>());
            }
        }

        public void Setup(IDamageable damageable)
        {
            _damageable = damageable;
            _objectToFollow = _damageable.GetGameObject();
            SetMaxValue(_damageable.GetHealth());
        }
        
        private void SetMaxValue(float value)
        {
            _maxValue = value;
        }

        private void SetValue(float value)
        {
            _currentValue = value;
            UpdateFillBar();
        }

        private void UpdateFillBar()
        {
            _fillImage.fillAmount = _currentValue / _maxValue;
        }
        
        private void Update()
        {
            if (_objectToFollow == null)
            {
                return;
            }
            
            Vector3 pos = Camera.main.WorldToScreenPoint(_objectToFollow.transform.position);
            transform.position = pos + _playerUIOffset;
            SetValue(_damageable.GetHealth());
        }
    }
}