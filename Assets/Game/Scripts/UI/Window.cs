using System;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class Window : MonoBehaviour
    {
        public static Action<Window> OnWindowClosed;

        public virtual void Start()
        {
        
        }

        public virtual void OnDestroyWindow()
        {
            OnWindowClosed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}