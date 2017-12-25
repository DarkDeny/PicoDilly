using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PicoDilly {
    public class Container : IContainer {
        private readonly Dictionary<Type, object> _dict;
        private Type _currentConfigurableItem;

        public Container() {
            _dict = new Dictionary<Type, Object>();
        }

        public T GetInstance<T>() {
            return (T)GetInstance(typeof(T));
        }

        private object[] GetConstructorArgs(ConstructorInfo ctor) {
            var list = new List<object>();
            foreach (var parameterInfo in ctor.GetParameters()) {
                var dependencyType = parameterInfo.ParameterType;
                var dependency = GetInstance(dependencyType);
                list.Add(dependency);
            }

            return list.ToArray();
        }

        private object GetInstance(Type dependencyType) {
            if (_dict.ContainsKey(dependencyType)) {
                return _dict[dependencyType];
            }

            object[] ctorArgs;
            var constructor = dependencyType.GetConstructor(Type.EmptyTypes);
            if (null != constructor) {
                ctorArgs = new object[] { };
            } else {
                constructor = dependencyType.GetConstructors().FirstOrDefault();
                if (null == constructor) {
                    throw new DependencyResolvingException($"Cannot create instance of type {dependencyType.Name}." +
                                                           $"\nDid you miss to register something for this type?");
                }
                ctorArgs = GetConstructorArgs(constructor);
            }

            var result = constructor.Invoke(ctorArgs);
            return result;
        }

        public void Configure(Action<IContainer> configureAction) {
            configureAction(this);
        }

        public IContainer For<T>() {
            _currentConfigurableItem = typeof(T);
            return this;
        }

        public IContainer Use<T>(T instance) {
            if (null == _currentConfigurableItem) {
                throw new InvalidOperationException();
            }
            _dict.Add(_currentConfigurableItem, instance);
            _currentConfigurableItem = null;
            return this;
        }
    }
}