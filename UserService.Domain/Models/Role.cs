using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace UserService.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        [EnumMember(Value = "User")]
        User = 1,
        [EnumMember(Value = "Super User")]
        SuperUser = 2,
        [EnumMember(Value = "Moderator")]
        Moderator = 3,
        [EnumMember(Value = "Administrator")]
        Administrator = 4
    }
}
