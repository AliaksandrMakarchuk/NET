using System;
using System.Collections.Generic;

namespace Funq
{
    public class Container
    {
        private Dictionary<Type, object> _factories = new Dictionary<Type, object>();

        public void Register<TService>(Func<Container, TService> factory)
        {
            _factories.Add(typeof(TService), factory);
        }

        public TService Resolve<TService>()
        {
            object factory = _factories[typeof(TService)];

            return ((Func<Container, TService>)factory).Invoke(this);
        }
    }
}