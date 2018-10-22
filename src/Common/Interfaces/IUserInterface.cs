using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.Common.Interfaces
{
    public interface IUserInterface
    {
        IAlgorithmItem GetAlgorithm(IEnumerable<IAlgorithmItem> algorithms);
        JObject GetInput(IAlgorithmItem selectedAlgorithm);
        void DisplayOutput(JObject jObject);
    }
}
