using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models
{
    [Table("role")]
    public partial class Role : BaseModel
    {
        [PrimaryKey("role_id", false)]
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }

        [JsonPropertyName("role_name")]
        public string RoleName { get; set; } = null!;

        [JsonPropertyName("role_image")]
        public string RoleImage { get; set; } = null!;
    }
}
