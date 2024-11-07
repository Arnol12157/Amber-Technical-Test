using System;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    using ServiceLocator;
    using ObjectPooling;
    
    public class BulletBase : MonoBehaviour, IBullet
    {
        protected ObjectPooling _objectPooling;

        private void Awake()
        {
            _objectPooling = ServiceLocator.GetService<ObjectPooling>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        public void Shoot()
        {
            OnShoot();
        }

        protected virtual void OnShoot()
        {
            
        }
        
        private void OnBecameInvisible()
        {
            _objectPooling.ReleaseObject(gameObject);
        }
    }
}