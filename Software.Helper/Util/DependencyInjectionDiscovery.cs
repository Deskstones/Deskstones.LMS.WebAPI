namespace Software.Helper.Util
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjectionDiscovery
    {
        private static void RegisterServicesFromDIConfigs(IServiceCollection services, Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                // Find all types that are named 'DependencyInjectionConfig'
                var diConfigTypes = assembly.GetTypes()
                                             .Where(t => t.Name == "DependencyInjectionConfig" && t.IsClass)
                                             .ToList();

                foreach (var diConfigType in diConfigTypes)
                {
                    // Find the AddServices method in the class and invoke it
                    var addServicesMethod = diConfigType.GetMethod("AddServices", BindingFlags.Public | BindingFlags.Static);

                    if (addServicesMethod != null)
                    {
                        // Invoke the AddServices method passing the services collection
                        addServicesMethod.Invoke(null, new object[] { services });
                    }
                }
            }
        }

        // Public method to register all DI services across multiple assemblies
        public static void RegisterAllDI(this IServiceCollection services)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();

            foreach (var path in toLoad)
            {
                try
                {
                    loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
                }
                catch
                {
                    // Log or skip invalid assemblies
                }
            }

            RegisterServicesFromDIConfigs(services, loadedAssemblies.ToArray());
        }
    }
}
