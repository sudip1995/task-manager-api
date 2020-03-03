using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace TaskManager.Library.Ioc
{
    public class IocContainer: IIocContainer
    {
        private static IocContainer _instance;
        private static readonly object LockObject = new object();
        private CompositionHost Container { get; set; }
        private ContainerConfiguration ContainerConfiguration { get; }
        private List<string> AssemblyLists { get; }
        private IocContainer()
        {
            ContainerConfiguration = new ContainerConfiguration();
            AssemblyLists = new List<string>();
            AddAssembly(Assembly.GetExecutingAssembly());
        }

        public void AddAssembly(Assembly assembly)
        {
            if (AssemblyLists.Contains(assembly.FullName))
            {
                return;
            }

            ContainerConfiguration.WithAssembly(assembly);
            AssemblyLists.Add(assembly.FullName);
        }

        public static IocContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new IocContainer();
                        }
                    }
                }

                return _instance;
            }
        }

        public T Resolve<T>()
        {
            Create();
            lock (LockObject)
            {
                try
                {
                    var value = Container.GetExport<T>();
                    return value;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to resolve type {0}", typeof(T).Name);
                    throw;
                }
            }
        }

        public void RegisterSingleton<TI, T>()
        {
            var conventions = new ConventionBuilder();
            conventions.ForType<T>().Export<TI>().Shared();
            ContainerConfiguration.WithPart(typeof(T), conventions);
        }

        private void Create()
        {
            if (Container == null)
            {
                lock (LockObject)
                {
                    if (Container == null)
                    {
                        Container = ContainerConfiguration.CreateContainer();
                    }
                }
            }
        }

        public void AddAllAssemblies()
        {
            AddAllAssemblies(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location));
            AddAllAssemblies(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        private void AddAllAssemblies(string location)
        {
            if (string.IsNullOrEmpty(location) == false)
            {
                var files = Directory.GetFiles(location);
                foreach (var file in files)
                {
                    try
                    {
                        var fileInfo =  new FileInfo(file);
                        if (fileInfo.Name.StartsWith("TaskManager", StringComparison.InvariantCultureIgnoreCase) && (fileInfo.Extension == ".dll" || fileInfo.Extension == ".exe"))
                        {
                            var assemblyName = Path.GetFileNameWithoutExtension(file);
                            if (string.IsNullOrEmpty(assemblyName) == false)
                            {
                                var assembly = Assembly.Load(assemblyName);
                                if (assembly != null)
                                {
                                    AddAssembly(assembly);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }
    }
}