using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Net.Assembly
{
    public class AssemblyFinder
    {
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^Autofac|^AutoMapper|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|netstandard|Azure.*";

        public string ClassNameSkipLoad { get; private set; } = "^System|^Microsoft|netstandard";

        public virtual AppDomain App => AppDomain.CurrentDomain;

        protected IList<System.Reflection.Assembly> GetAssemblies()
        {
            var loadedAssemblyNames = new List<string>();

            foreach (var assembly in App.GetAssemblies())
            {
                if (String.IsNullOrEmpty(assembly.FullName) || Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern)) continue;
                loadedAssemblyNames.Add(assembly.FullName);
            }

            LoadMatchingAssemblies(GetBinDirectory(), loadedAssemblyNames);

            return App.GetAssemblies()
                            .Where(assembly => !String.IsNullOrEmpty(assembly.FullName) && !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern)).Distinct().ToList();
        }

        public virtual string GetBinDirectory()
        {
            return AppContext.BaseDirectory;
        }

        protected virtual void LoadMatchingAssemblies(string directoryPath, List<string> iAssemblyLoaded)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            foreach (var dllPath in Directory.GetFileSystemEntries(directoryPath, "*.dll"))
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllPath);
                    if (!Regex.IsMatch(an.FullName, AssemblySkipLoadingPattern) && !iAssemblyLoaded.Contains(an.FullName))
                    {
                        App.Load(an);
                    }
                }
                catch (BadImageFormatException ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
        }
    }
}
