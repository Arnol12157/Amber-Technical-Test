using System;
using System.Collections.Generic;
using Game.Scripts.UI.Hud;
using Game.Scripts.UI.Loser;
using Game.Scripts.UI.Winner;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class WindowGenerator : MonoBehaviour
    {
        public const string UI = "UI/";
        public const string WINNER_WINDOW = UI + "WinnerWindow";
        public const string LOSER_WINDOW = UI + "LoserWindow";
        public const string HUD_WINDOW = UI + "HudWindow";

        public GameObject HudWindow;

        private Dictionary<string, Component> _windowDictionary = new();

        public T GetOpenedWindowByURL<T>(string path) where T : Component
        {
            if (!_windowDictionary.ContainsKey(path))
            {
                return null;
            }

            return (T)_windowDictionary[path];
        }
        
        private T GetNewOrRecycledWindow<T>(string path, bool forceInstantiation = false) where T : Component
        {
            T result = null;
            if (!forceInstantiation)
            {
                result = GetOpenedWindowByURL<T>(path);
            }

            if (result == null)
            {
                result = GetFromResources<T>(path);

                if (_windowDictionary.ContainsKey(path))
                {
                    _windowDictionary[path] = result;
                }
                else
                {
                    _windowDictionary.Add(path, result);
                }
            }
            result.transform.SetAsLastSibling();

            return result;
        }
        
        private T GetFromResources<T>(string path) where T : Component
        {
            GameObject window = Instantiate(Resources.Load<GameObject>(path), transform.position, Quaternion.identity);

            if (window == null)
            {
                throw new Exception($"Window not found at path {path}.");
            }

            if (window.TryGetComponent(out T result))
            {
                return result;
            }

            throw new Exception($"Component of type {nameof(T)} was not found in game object {window.name}.");
        }
        
        public WinnerWindow ShowWinnerWindow()
        {
            return GetNewOrRecycledWindow<WinnerWindow>(WINNER_WINDOW);
        }
        
        public LoserWindow ShowLoserWindow()
        {
            return GetNewOrRecycledWindow<LoserWindow>(LOSER_WINDOW);
        }

        public HudWindow GetHudWindow()
        {
            return HudWindow.GetComponent<HudWindow>();
        }
    }
}