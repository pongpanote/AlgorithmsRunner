using System.Collections.Generic;
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

        public static IEnumerable<PropertyInfo> GetPropertiesInfos<T>(this T type) where T : class
        {
            return type.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
