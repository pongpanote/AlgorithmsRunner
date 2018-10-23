using System;
using AlgorithmsRunner.Common.Properties;

namespace AlgorithmsRunner.Common.Schemas
{
    public static class SchemaHelper
    {
        public static string GetSchema(string guid)
        {
            guid = guid.ToUpperInvariant();

            if (guid == Constants.SEQUENCE_ANALYSIS_GUID)
            {
                return Resources.json_schema_SequenceAnalysis;
            }

            if (guid == Constants.SUM_OF_MULTIPLE_GUID)
            {
                return Resources.json_schema_SumOfMultiple;
            }

            throw new ArgumentException($"Cannot resolve schema for the algorithm with GUID='{guid}'");
        }
    }
}