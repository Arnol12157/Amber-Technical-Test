using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Animations;

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