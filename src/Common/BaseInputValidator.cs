using AlgorithmsRunner.Common.Interfaces;
using AlgorithmsRunner.Common.Schemas;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace AlgorithmsRunner.Common
{
    public abstract class BaseInputValidator
    {
        protected void Validate<T>(JObject inputJObject) where T : IAlgorithmItem
        {
            var schema = JSchema.Parse(SchemaHelper.GetSchema(typeof(T).GUID.ToString()));
            if (!inputJObject.IsValid(schema))
            {
                throw new JSchemaValidationException("Input format was invalid against JSON schema.");
            }
        }
    }
}
