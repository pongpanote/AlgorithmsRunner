using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.Common
{
    public interface IAlgorithmItem
    {
        JObject Process(JObject inputJObject);
        string GetDisplayName();
        string GetDescription();
    }
}
