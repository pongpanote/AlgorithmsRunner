using System;

namespace AlgorithmsRunner.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyUsageAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}