using System;

namespace AlgorithmsRunner.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputAttribute : Attribute
    {
        private string m_DisplayName;
        public string InputName { get; set; }

        public string DisplayName
        {
            get => m_DisplayName ?? InputName;
            set => m_DisplayName = value;
        }
    }
}