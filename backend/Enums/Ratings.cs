using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace backend.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Ratings
    {
        [EnumMember(Value = "S")]
        S,
        [EnumMember(Value = "A")]
        A,
        [EnumMember(Value = "B")]
        B,
        [EnumMember(Value = "C")]
        C,
        [EnumMember(Value = "D")]
        D,
        [EnumMember(Value = "E")]
        E,
        [EnumMember(Value = "F")]
        F
    }
}
