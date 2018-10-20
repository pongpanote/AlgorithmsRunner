using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.Common
{
    public interface IAlgorithmItem
    {
        JObject Run(JObject inputJObject);
        string GetDisplayName();
        string GetDescription();
    }
}
