using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Assembly
{
    public interface ITypeFinder
    {
        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <returns>Result</returns>
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <param name="assignTypeFrom">Assign type from</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <returns>Result</returns>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
    }
}
