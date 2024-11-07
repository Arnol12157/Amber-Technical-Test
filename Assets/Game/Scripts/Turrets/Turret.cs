using System.Collections;
using Game.Scripts.Bullets;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Turrets
{
    using ServiceLocator;
    using SpawnSystem;
    using ObjectPooling;
    
    public class Turret : MonoBehaviour, IShooter
    {
        [SerializeField] private TurretModel _turretModel;
        private TurretData _data;
        private SpawnSystem _spawnSystem;
        private ObjectPooling _objectPooling;
        private GameManager _gameManager;

        private void Start()
        {
            SetupServices();
            
            _data = _turretModel.Data;
            StartCoroutine(Shoot());
        }

        private void SetupServices()
        {
            _spawnSystem = ServiceLocator.GetService<SpawnSystem>();
            _objectPooling = ServiceLocator.GetService<ObjectPooling>();
            _gameManager = ServiceLocator.GetService<GameManager>();
        }

        public void OnShoot()
        {
            if (_gameManager.IsGameOver())
            {
                return;
            }

            GameObject bullet = _spawnSystem.SpawnOnPosition(ObjectPoolModel.PoolType.NormalBullet,
                transform.position);
            bullet.GetComponent<IBullet>().Shoot();
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(_data.BulletSpawnDelay);
                if (_objectPooling.AreActiveEnemies())
                {
                    OnShoot();
                }
            }
        }
    }
}