using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game.Scripts.ServiceLocator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void RegisterService<T>(T service)
        {
            Type serviceType = typeof(T);
            
            Assert.IsFalse(_services.ContainsKey(serviceType));
            
            _services.Add(serviceType, service);
        }

        public static T GetService<T>()
        {
            Type serviceType = typeof(T);
            if (_services.ContainsKey(serviceType))
            {
                return (T)_services[serviceType];
            }
            throw new Exception("Service not founded");
        }
    }
}