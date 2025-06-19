using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;                             // for the Supabase client
using Newtonsoft.Json.Converters;

namespace backend.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
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
