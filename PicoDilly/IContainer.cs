using System;
using System.Reflection;

namespace PicoDilly {
    public interface IContainer {
        T GetInstance<T>();
        void Configure(Action<IContainer> configureAction);
        IContainer For<T>();
        IContainer Use<T>(T instance);
    }
}