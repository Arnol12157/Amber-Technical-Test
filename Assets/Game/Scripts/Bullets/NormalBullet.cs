using System;
using System.Collections.Generic;
using Game.Scripts.Enemies;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    public class NormalBullet : BulletBase
    {
        [SerializeField] private BulletModel _bulletModel;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _direction;
        private BulletData _data;

        protected override void OnAwake()
        {
            base.OnAwake();
            _data = _bulletModel.Data;
        }

        protected override void OnShoot()
        {
            base.OnShoot();
            Vector3 direction = _objectPooling.GetClosestEnemy().transform.position - transform.position;
            _direction = direction.normalized;
            _rigidbody.velocity = _direction * _data.Speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null && other.GetComponent<Base.Base>() == null)
            {
                other.GetComponent<IDamageable>().DoDamage(_data.DamageAmount);
                _objectPooling.ReleaseObject(gameObject);
            }
        }
    }
}