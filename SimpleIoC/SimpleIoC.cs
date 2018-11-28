using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIoC
{
    /// <summary>
    /// A very simple IoC container
    /// </summary>
    class SimpleIoC
    {
        /// <summary>
        /// Dependency registry
        /// </summary>
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers a dependency by adding it to the map
        /// </summary>
        /// <typeparam name="TFrom">Parent dependency</typeparam>
        /// <typeparam name="TTo">Child dependency</typeparam>
        public void Register<TFrom, TTo>()
        {
            _map.Add(typeof(TFrom), typeof(TTo));
        }

        /// <summary>
        /// Resolves a Type the client wants to resolve
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Gets the type from the map and throws an exception if it cant find it
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Resolve(Type type)
        {
            Type resolvedType = null;
            try
            {
                resolvedType = _map[type];
            }
            catch
            {
                throw new ArgumentException($"Couldn't resolve type: {type}");
            }

            var ctor = resolvedType.GetConstructors().First();
            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length == 0)
                return Activator.CreateInstance(resolvedType);

            var parameters = new List<object>();
            foreach (var parameterToResolve in ctorParameters)
            {
                parameters.Add(Resolve(parameterToResolve.ParameterType));
            }

            return ctor.Invoke(parameters.ToArray());
        }
    }
}
