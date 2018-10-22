using AlgorithmsRunner.Common;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsRunner.Tests
{
    public class CommonTest
    {
        private Type1 m_Type1;

        [OneTimeSetUp]
        public void SetUp()
        {
            m_Type1 = new Type1();
        }

        [Test]
        public void GetAttributeOfType()
        {
            var expectedResult = new List<InputAttribute>
            {
                new InputAttribute{InputName = "Input Name1", DisplayName = "Display Name1"},
                new InputAttribute{InputName = "Input Name2"},
                new InputAttribute{InputName = "Input Name3", DisplayName = "Display Name3"}
            };

            var propertiesList = m_Type1.GetPropertiesInfos().Select(y => y.GetAttributeOfType<InputAttribute>()).Where(y => y != null).ToList();

            propertiesList.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetPropertiesInfos()
        {
            var expectedResult = new List<string>
            {
                "Input1",
                "Input2",
                "Input3",
                "Input4"
            };

            var propertiesName = new List<string>();
            var propertiesList = m_Type1.GetPropertiesInfos().ToList();

            foreach (var property in propertiesList)
            {
                propertiesName.Add(property.Name);
            }

            propertiesName.Should().BeEquivalentTo(expectedResult);
        }

        private class Type1
        {
            [Input(InputName = "Input Name1", DisplayName = "Display Name1")]
            public string Input1 { get; set; }

            [Input(InputName = "Input Name2")]
            public string Input2 { get; set; }

            [Input(InputName = "Input Name3", DisplayName = "Display Name3")]
            private string Input3 { get; set; }

            protected string Input4 { get; set; }
        }
    }
}
