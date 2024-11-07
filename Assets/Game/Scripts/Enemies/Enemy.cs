using Game.Scripts.Economy;
using Game.Scripts.GameLogic;
using Game.Scripts.Interfaces;
using Game.Scripts.UI;
using Game.Scripts.UI.HealthBar;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    using ServiceLocator;
    using ObjectPooling;
    using SpawnSystem;

    public class Enemy : MonoBehaviour, IDamageable, IFreezeable
    {
        [SerializeField] private EnemyModel _enemyData;
        [SerializeField] private EnemyMovement _enemyMovement;
        private EnemyData _data = new EnemyData();
        private HealthBar _healthBar;
        private ObjectPooling _objectPooling;
        private SpawnSystem _spawnSystem;
        private EconomyManager _economyManager;
        private UIManager _uiManager;
        private GameManager _gameManager;
        
        public EnemyData EnemyData
        {
            get => _data;
            set => _data = value;
        }

        private void Start()
        {
            SetupServices();

            EnemyData = _enemyData.Data;
            _enemyMovement.CurrentSpeed = _data.Speed;
            SetupHealthBar();
            _enemyMovement.Setup(_gameManager.Base);
        }

        private void SetupServices()
        {
            _objectPooling = ServiceLocator.GetService<ObjectPooling>();
            _spawnSystem = ServiceLocator.GetService<SpawnSystem>();
            _economyManager = ServiceLocator.GetService<EconomyManager>();
            _uiManager = ServiceLocator.GetService<UIManager>();
            _gameManager = ServiceLocator.GetService<GameManager>();
        }

        private void Update()
        {
            _enemyMovement.CanMove = !_gameManager.IsGameOver();
        }

        private void OnEnable()
        {
            SetupHealthBar();
        }

        private void OnDisable()
        {
            if (_healthBar != null)
            {
                _objectPooling.ReleaseObject(_healthBar.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.DoDamage(_data.DamageToBase);
                _objectPooling.ReleaseObject(gameObject);
            }
        }

        public void DoDamage(int damageToEnemy)
        {
            _data.HitPoints -= damageToEnemy;
            if (_data.HitPoints <= 0)
            {
                _economyManager.AddCoins(_data.GetCoinsReward());
                _objectPooling.ReleaseObject(this.gameObject);
            }
        }

        public void DoFreeze()
        {
            _enemyMovement.CurrentSpeed = _data.SpeedFreeze;
        }
        
        private void SetupHealthBar()
        {
            if (_data.Equals(default(EnemyData)))
            {
                return;
            }

            GameObject healthBar = _spawnSystem.Spawn(ObjectPoolModel.PoolType.HealthBar);
            healthBar.transform.SetParent(_uiManager.GetHudWindow().transform);
            _healthBar = healthBar.GetComponent<HealthBar>();
            _healthBar.Setup(this);
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