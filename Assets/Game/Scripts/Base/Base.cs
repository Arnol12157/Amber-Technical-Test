using System;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Base
{
    public class Base : MonoBehaviour, IDamageable
    {
        [Tooltip("Reference to the Base Model")]
        [SerializeField] private BaseModel _baseModel;
        private BaseData _data;
        public Action OnBaseDestroyed;

        private void Awake()
        {
            _data = _baseModel.Data;
        }

        public void DoDamage(int damageToBase)
        {
            _data.HitPoints -= damageToBase;

            if (_data.HitPoints <= 0)
            {
                OnBaseDestroyed?.Invoke();
            }
        }

        public int GetHealth()
        {
            return _data.HitPoints;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}