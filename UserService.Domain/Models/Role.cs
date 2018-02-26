using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
        [EnumMember(Value = "Admin")]
        Admin = 4
    }
}
