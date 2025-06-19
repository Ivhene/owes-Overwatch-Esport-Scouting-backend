using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;                             // for the Supabase client
using Newtonsoft.Json.Converters;

namespace backend.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Regions
    {
        [EnumMember(Value = "NA")]
        NA,
        [EnumMember(Value = "EMEA")]
        EMEA,
        [EnumMember(Value = "Japan")]
        Japan,
        [EnumMember(Value = "Korea")]
        Korea,
        [EnumMember(Value = "Pacific")]
        Pacific
    }
}
