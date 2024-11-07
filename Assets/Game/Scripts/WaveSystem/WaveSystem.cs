using System.Collections;
using System.Collections.Generic;
using Game.Scripts.DependencyInjection;
using Game.Scripts.Enemies;
using Game.Scripts.SpawnSystem;
using UnityEngine;

namespace Game.Scripts.WaveSystem
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField] private WaveModel Wave;
        [Inject] private GameManager _gameManager;
        [Inject] private SpawnSystem.SpawnSystem _spawnSystem;
        private int _waveIndex;
        private int _enemyQuantity;
        private bool _finishWaveCreation;

        public bool FinishWaveCreation
        {
            get => _finishWaveCreation;
            private set => _finishWaveCreation = value;
        }

        void Start()
        {
            StartCoroutine(DoWave());
        }

        private IEnumerator DoWave()
        {
            while (Wave.WaveData[_waveIndex].WaveQuantity > _enemyQuantity)
            {
                if (_gameManager.IsGameOver())
                {
                    yield break;
                }

                yield return new WaitForSeconds(Wave.WaveData[_waveIndex].WaveDelay);
                GameObject obj = _spawnSystem.Spawn(Wave.WaveData[_waveIndex].WaveEnemy.Data.Type);
                obj.GetComponent<Enemy>().EnemyData = Wave.WaveData[_waveIndex].WaveEnemy.Data;
                _enemyQuantity++;
            }

            _waveIndex++;
            if (Wave.WaveData.Count > _waveIndex)
            {
                _enemyQuantity = 0;
                StartCoroutine(DoWave());
            }
            else
            {
                FinishWaveCreation = true;
            }
        }
    }
}
