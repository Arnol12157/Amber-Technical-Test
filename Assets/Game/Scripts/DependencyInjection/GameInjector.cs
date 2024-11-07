using System.Reflection;
using Game.Scripts.DependencyInjection;
using Game.Scripts.Economy;
using Game.Scripts.Turrets;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    using ServiceLocator;
    using SpawnSystem;
    using WaveSystem;
    using ObjectPooling;

    public class GameInjector : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private EconomyManager _economyManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private TurretsSystem _turretsSystem;
        [SerializeField] private SpawnSystem _spawnSystem;
        [SerializeField] private WaveSystem _waveSystem;
        [SerializeField] private ObjectPooling _objectPooling;
        [SerializeField] private WindowGenerator _windowGenerator;

        private void Awake()
        {
            InjectAllDependencies();
            RegisterAllServices();
        }

        private void InjectAllDependencies()
        {
            InjectDependencies(_turretsSystem, _economyManager, _spawnSystem);
            InjectDependencies(_spawnSystem, _objectPooling);
            InjectDependencies(_gameManager, _turretsSystem, _economyManager, _waveSystem, _uiManager, _objectPooling);
            InjectDependencies(_uiManager, _economyManager, _windowGenerator, _gameManager);
            InjectDependencies(_waveSystem, _gameManager, _spawnSystem);
        }

        private void RegisterAllServices()
        {
            ServiceLocator.RegisterService<ObjectPooling>(_objectPooling);
            ServiceLocator.RegisterService<SpawnSystem>(_spawnSystem);
            ServiceLocator.RegisterService<GameManager>(_gameManager);
            ServiceLocator.RegisterService<EconomyManager>(_economyManager);
            ServiceLocator.RegisterService<UIManager>(_uiManager);
            ServiceLocator.RegisterService<TurretsSystem>(_turretsSystem);
        }

        private void InjectDependencies(object target, params object[] dependencies)
        {
            var targetType = target.GetType();
            var injectFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var injectProperties = targetType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var dependency in dependencies)
            {
                foreach (var field in injectFields)
                {
                    if (field.GetCustomAttribute<InjectAttribute>() != null && field.FieldType.IsInstanceOfType(dependency))
                    {
                        field.SetValue(target, dependency);
                    }
                }

                foreach (var property in injectProperties)
                {
                    if (property.GetCustomAttribute<InjectAttribute>() != null && property.PropertyType.IsInstanceOfType(dependency))
                    {
                        property.SetValue(target, dependency);
                    }
                }
            }
        }
    }
}