using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Net.Assembly
{
    public class TypeFinder : AssemblyFinder, ITypeFinder
    {
        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            var _result = new List<Type>();

            // Push assembly to Thread to run 
            Parallel.ForEach(GetAssemblies(), (assembly) =>
            {
                Type[] types = assembly.GetTypes();

                if (types == null) return;

                foreach (Type type in types)
                {
                    if (Regex.IsMatch(ClassNameSkipLoad, type.FullName?? "")) 
                        continue;

                    if (!assignTypeFrom.IsAssignableFrom(type) && (!assignTypeFrom.IsGenericTypeDefinition || !Matches(type, assignTypeFrom)))
                    {
                        continue;
                    }

                    if (type.IsInterface) 
                        continue;

                    if (onlyConcreteClasses)
                    {
                        if (type.IsClass && !type.IsAbstract)
                        {
                            _result.Add(type);
                        }
                    }
                    else
                    {
                        _result.Add(type);
                    }
                }
            });

            return _result;
        }

        private bool Matches(Type source, Type target)
        {
            var filters = source.FindInterfaces((objType, objCriteria) => true, null);

            foreach (var filter in filters)
            {
                if (filter.IsGenericType) continue;

                return filter.IsAssignableFrom(target);
            }
            return false;
        }
    }
}
