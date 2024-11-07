using System;
using System.Collections.Generic;
using Game.Scripts.Base;
using Game.Scripts.DependencyInjection;
using Game.Scripts.Economy;
using Game.Scripts.GameLogic;
using Game.Scripts.ObjectPooling;
using Game.Scripts.Turrets;
using Game.Scripts.WaveSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Lose,
        Win
    }
    
    [SerializeField] private GameObject _base;
    [SerializeField] private GameState _state;
    private List<IGameStateObserver> _gameStateObservers = new();
    
    [Inject] private TurretsSystem _turretsSystem;
    [Inject] private EconomyManager _economyManager;
    [Inject] private WaveSystem _waveSystem;
    [Inject] private UIManager _uiManager;
    [Inject] private ObjectPooling _objectPooling;

    public TurretsSystem TurretsSystem
    {
        get => _turretsSystem;
        set => _turretsSystem = value;
    }

    public GameObject Base => _base;

    public GameState State
    {
        get => _state;
        set => _state = value;
    }

    private void Start()
    {
        _base.GetComponent<Base>().OnBaseDestroyed += OnBaseDestroyed;
    }

    private void OnBaseDestroyed()
    {
        UpdateState(GameState.Lose);
    }

    private void Update()
    {
        if (!_objectPooling.AreActiveEnemies() && _waveSystem.FinishWaveCreation)
        {
            UpdateState(GameState.Win);
        }
    }

    public void SubscribeToGameStateChanges(IGameStateObserver observer)
    {
        _gameStateObservers.Add(observer);
    }
    
    public void UnsubscribeToGameStateChanges(IGameStateObserver observer)
    {
        _gameStateObservers.Remove(observer);
    }
    
    private void NotifyGameStateUpdate()
    {
        foreach (var observer in _gameStateObservers)
        {
            observer.OnGameStateUpdated(State);
        }
    }

    public void UpdateState(GameState newState)
    {
        if (State == newState)
        {
            return;
        }
        
        State = newState;
        NotifyGameStateUpdate();
    }

    public bool IsGameOver()
    {
        return State == GameState.Lose;
    }
}
