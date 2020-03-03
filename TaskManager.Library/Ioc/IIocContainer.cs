using System;
using System.Reflection;

namespace TaskManager.Library.Ioc
{
    public interface IIocContainer
    {
        void AddAllAssemblies();
        void AddAssembly(Assembly assembly);
        T Resolve<T>();
        void RegisterSingleton<TI, T>();
    }
}