using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace backend.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
