using System.Linq;
using System.Reflection;

namespace AlgorithmsRunner.Common
{
    public static class Extensions
    {
        public static T GetAttributeOfType<T>(this MemberInfo type)
        {
            return type.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
        }
    }
}
